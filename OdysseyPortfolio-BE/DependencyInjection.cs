using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using OdysseyPortfolio_Libraries.Migrations;
using OdysseyPortfolio_Libraries.Repositories;

namespace OdysseyPortfolio_BE
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }           
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<OdysseyPortfolioDbContext>(options => options.UseSqlServer(GetConnectionString()));
            return services;
        }
        //public static IServiceCollection AddServices(this IServiceCollection services)
        //{

        //    services.AddScoped<IAuthenticationService, AuthenticationService>();
        //    services.AddScoped<IStudentService, StudentService>();
        //    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //    return services;
        //}
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
            });
            });
            return services;
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn;
        }
    }

}

