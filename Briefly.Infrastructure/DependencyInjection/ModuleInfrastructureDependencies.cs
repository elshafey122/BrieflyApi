using Briefly.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Briefly.Infrastructure.DepencyInjection
{
    public static class ConfigurationRepository
    {
        public static IServiceCollection AddRepositoryDependinces(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 
            services.AddTransient<IRssRepository, RssRepository>();
            services.AddTransient<IRssSubscribRepository, RssSubscribRepository>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<ICommentsRepository, CommentRepository>();

            return services;
        }
    }
}
