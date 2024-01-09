using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Application.ValidationBehaviour;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Application.Login;
using Application.Authentication.Login;

namespace Application.ApplicationDependencyInjection
{
    public static class ApplicationDependencyInjection 
    {
        public static object AddApplication(this IServiceCollection services, IConfiguration configuration,string key)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IJsonWebToken, JsonWebToken>();
            services.AddScoped<IDbConnection>((x) =>  new SqlConnection(configuration.GetConnectionString("DevConnection")) );
            return services;
        }
    }
}
