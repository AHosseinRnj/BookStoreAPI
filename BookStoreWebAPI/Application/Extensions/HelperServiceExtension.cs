using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class HelperServiceExtension
    {
        public static void ConfigureApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthorReadService, AuthorReadService>();
            services.AddScoped<IAuthorWriteService, AuthorWriteService>();

            services.AddScoped<IBookReadService, BookReadService>();
            services.AddScoped<IBookWriteService, BookWriteService>();

            services.AddScoped<ICategoryReadService, CategoryReadService>();
            services.AddScoped<ICategoryWriteService, CategoryWriteService>();

            services.AddScoped<IOrderItemReadService, OrderItemReadService>();
            services.AddScoped<IOrderItemWriteService, OrderItemWriteService>();

            services.AddScoped<IOrderReadService, OrderReadService>();
            services.AddScoped<IOrderWriteService, OrderWriteService>();

            services.AddScoped<IPublisherReadService, PublisherReadService>();
            services.AddScoped<IPublisherWriteService, PublisherWriteService>();

            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IUserWriteService, UserWriteService>();
        }
    }
}