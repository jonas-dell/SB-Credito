using Microsoft.Extensions.DependencyInjection;
using SB.Core.Mediator;
using SB.Credito.Domain.Repositories;
using SB.Credito.Infra;
using SB.Credito.Infra.Repositories;

namespace SB.Credito.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<ICreditoRepository, CreditoRepository>();

            services.AddScoped<CreditoContext>();
        }
    }
}
