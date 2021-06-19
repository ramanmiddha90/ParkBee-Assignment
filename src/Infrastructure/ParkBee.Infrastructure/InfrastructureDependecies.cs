using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkBee.Core.Application.Common.Interfaces;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Infrastructure.Repositories;
using ParkBee.Infrastructure.Context;
using ParkBee.Infrastructure.Services;

namespace ParkBee.Infrastructure
{
    public static class InfrastructureDependecies
    {
        public static IServiceCollection RegisterInfrastrucureDependecies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ParkBeeDataBase")));

            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
            services.AddScoped<IGarageRepository, GarageRepository>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
