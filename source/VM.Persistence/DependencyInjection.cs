using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using VM.Domain.Abstractions;
using VM.Persistence.Interceptors;
using VM.Persistence.Repositories;

namespace VM.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string Database = configuration.GetConnectionString("Database")!;

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICartItemRepository, CartItemRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICouponRepository, CouponRepository>();
        services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IRatingRepository, RatingRepository>();
        services.AddTransient<IShippingRepository, ShippingRepository>();
        services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) =>
            {
                options.UseSqlServer(Database);
            });

        return services;
    }
}
