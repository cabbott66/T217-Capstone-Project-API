
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using T217_Capstone_Project_API.Authentication;
using T217_Capstone_Project_API.Repositories;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configure Kestrel to listen on port 5000 (internal HTTP)
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5000);
            });

            builder.Services.AddControllers();

            // Database Context.
            var connectionString = builder.Configuration.GetConnectionString("stakeholderRisksDb");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            builder.Services.AddDbContext<StakeholderRisksContext>(
                dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion),
                ServiceLifetime.Transient);

            // Repositories.
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectUserRepository, ProjectUserRepository>();
            builder.Services.AddScoped<IStakeholderRepository, StakeholderRepository>();
            builder.Services.AddScoped<IRisksRepository, RisksRepository>();

            // Filters
            builder.Services.AddScoped<UserAuthenticationFilter>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "The API access key",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "x-api-key",
                    In = ParameterLocation.Header,
                    Scheme = "ApiKeyScheme"
                });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header,
                };
                var requirement = new OpenApiSecurityRequirement
                {
                    { scheme, new List<string> {} }
                };
                x.AddSecurityRequirement(requirement);
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StakeholderRisksContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use an environment variable for the allowed origins
            var allowedOrigins = builder.Configuration["AllowedOrigins"];
            string[] allowedOriginsArray = allowedOrigins?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? new string[] { "http://localhost:3000" };

            app.UseCors(policy =>
                policy.WithOrigins(allowedOriginsArray)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            // We can't use this for our setup
            // app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
