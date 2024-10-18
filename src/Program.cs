using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using src.Controllers.Services;
using AppContext = src.Models.AppContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
            builder.Services.AddScoped<StaffRepository>();
            builder.Services.AddScoped<OperationTypeRepository>();
            builder.Services.AddScoped<SpecializationRepository>();
            builder.Services.AddScoped<ManageStaffService>();
            builder.Services.AddScoped<AuthService>();
           
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
            builder.Services
            

            var app = builder.Build();

            // Ensure the database is created.
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppContext>();
                context.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.USeAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void RegisteredServices(IServiceCollection services)
        {

            // Repos
            services.AddScoped<StaffRepository>();
            services.AddScoped<OperationTypeRepository>();
            services.AddScoped<SpecializationRepository>();

            // Services
        }
    }
}