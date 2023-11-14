using Application;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;

namespace Infrastructure.Extensions
{
    public static class HelperServiceExtension
    {
        public static void ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<EFContext>(option => option.UseSqlServer(connectionString, b => b.MigrationsAssembly("WebAPI")));
            services.AddScoped<IEFUnitOfWork, EFUnitOfWork>();

            services.AddScoped<DapperContext>();
            services.AddScoped<IDapperUnitOfWork, DapperUnitOfWork>();

            services.AddScoped<IAuthorWriteRepository, AuthorWriteRepository>();
            services.AddScoped<IAuthorReadRepository, AuthorReadRepository>();

            services.AddScoped<IBookWriteRepository, BookWriteRepository>();
            services.AddScoped<IBookReadRepository, BookReadRepository>();

            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();

            services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();
            services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();

            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            services.AddScoped<IPublisherWriteRepository, PublisherWriteRepository>();
            services.AddScoped<IPublisherReadRepository, PublisherReadRepository>();

            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
        }
    }
}