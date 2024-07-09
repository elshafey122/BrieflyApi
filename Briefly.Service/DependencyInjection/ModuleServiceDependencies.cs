using Briefly.Service.implementations;
using Briefly.Service.Implemintations.Articles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Briefly.Infrastructure.DepencyInjection
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddServiceDependinces(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IRssService, RssService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IHangfireService, HangfireService>();
            services.AddHttpClient("AI Summerizar", confg => confg.BaseAddress = new Uri(configuration["SummerizeApi:Url"]!));
            return services;
        }
    }
}