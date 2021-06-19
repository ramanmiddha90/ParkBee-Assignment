using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ParkBee.Core.Application.Common.Behaviours;
using ParkBee.Core.Application.Garages.Commands;
using System.Reflection;
using FluentValidation;
using FluentValidation.Validators;

namespace ParkBee.Core.Application
{
    public static class CoreServicesDependencies
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
           
        }
    }
}
