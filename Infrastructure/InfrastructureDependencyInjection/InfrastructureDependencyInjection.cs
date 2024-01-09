using Domain.Interfaces;
using Domain;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Domain.DomainObjects;
using System.Data;

namespace Infrastructure.InfrastructureDependencyInjection
{
    public static class InfrastructureDependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
