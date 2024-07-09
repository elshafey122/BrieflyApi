using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Briefly.Core.DepencyInjection
{
    public static class ModuleCoreDependecies
    {
        public static IServiceCollection AddCoreDependinces(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
