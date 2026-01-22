using Microsoft.EntityFrameworkCore;
using WizCo.Application.Interfaces.Services;
using WizCo.Application.Services;
using WizCo.Domain.Interfaces.Repositories;
using WizCo.Infrastructure.Data;
using WizCo.Infrastructure.Repositories;
using WizCo.Infrastructure.Services;

namespace WizCo.Api.Extensions
{
    public static class InjectionDependenceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WizCoDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IServiceContext, ServiceContext>();

            return services;
        }
    }
}
