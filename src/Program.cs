using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using src.Controllers.Services;
using AppContext = src.Models.AppContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.StaffAggregate;
using Domain.AppointmentAggregate;
using Domain.PatientAggregate;
using Domain.OperationRequestAggregate;
using src.Services.IServices;
using src.Domain.Shared;
using src.Infrastructure;
using Domain.LogAggregate;
using Microsoft.OpenApi.Models;
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

            // Configure Kestrel to bind to a specific IP and port
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(System.Net.IPAddress.Any, 80); // Listen on all IPs, port 80 for HTTP
                options.Listen(System.Net.IPAddress.Any, 443, listenOptions =>
                {
                    listenOptions.UseHttps(); // Enable HTTPS if you want
                });
            });

            // Configure JWT authentication with AWS Cognito

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            builder.Services.ConfigureOptions<JwtBearerConfigureOptions>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // Register Repos and Services
            RegisteredServices(builder.Services);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Version = "v1" });

                // Add JWT Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }});
            });


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

            //app.UseHttpsRedirection();

            app.MapGet("/claims",
                    (ClaimsPrincipal claims) => claims.Claims.Select(c => new { c.Type, c.Value }).ToArray())
                .RequireAuthorization()
                .WithName("GetClaims");


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
            services.AddScoped<IPendingRequestRepository, PendingRequestRepository>();
            //services.AddScoped<ISurgeryRoomRepository, SurgeryRoomRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();


            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPlanningModuleService, PlanningModuleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEncryptionEmailService, EmailEncryptionService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IOperationTypeService, OperationTypeService>();
            services.AddScoped<IPendingRequestService, PendingRequestService>();
            services.AddScoped<IOperationRequestService, OperationRequestService>();
            services.AddScoped<ISensitiveDataService, SensitiveDataService>();
            services.AddScoped<IAvailabilitySlotService, AvailabilitySlotService>();

            //services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        }
    }
}