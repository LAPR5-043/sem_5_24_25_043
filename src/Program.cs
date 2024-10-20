using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using src.Controllers.Services;
using AppContext = src.Models.AppContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.StaffAggregate;
using Domain.OperationTypeAggregate;
using Domain.AppointmentAggregate;
using Domain.SurgeryRoomAggregate;
using src.Infrastructure.Repositories;
using Domain.SpecializationAggregate;
using Domain.PatientAggregate;
using Domain.OperationRequestAggregate;
using src.Services.IServices;
using src.Domain.Shared;
using src.Infrastructure;
using Domain.LogAggregate;
using src.Services.Services;
using src.Services;


namespace sem_5_24_25_043
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
             // Configure JWT authentication with AWS Cognito
            var cognitoAuthority = $"https://cognito-idp.{builder.Configuration["AWS:Region"]}.amazonaws.com/{builder.Configuration["AWS:Cognito:UserPoolId"]}";
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = cognitoAuthority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = cognitoAuthority,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AWS:Cognito:ClientId"],
                        ValidateLifetime = true,
                        NameClaimType = "email",
                        RoleClaimType = "cognito:groups"
                    };
                });
            
             // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // Register Repos and Services
            RegisteredServices(builder.Services);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            var app = builder.Build();

            // Ensure the database is created.
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppContext>();
                context.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API");
                    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });
            //}

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void RegisteredServices(IServiceCollection services)
        {

            // Repos
            //services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IOperationRequestRepository, OperationRequestRepository>();
            services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
            //services.AddScoped<ISpecializationRepository, SpecializationRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            //services.AddScoped<ISurgeryRoomRepository, SurgeryRoomRepository>();
            //services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            

            // Services
            services.AddScoped<AuthService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IOperationTypeService, OperationTypeService>();
            //services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IOperationRequestService, OperationRequestService>();
            //services.AddScoped<ISpecializationRepository, SpecializationRepository>();
            
        }
    }
}