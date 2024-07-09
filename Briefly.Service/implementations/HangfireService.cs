using CodeHollow.FeedReader;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Briefly.Service.implementations
{
    public class HangfireService : IHangfireService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IRssRepository _rssRepository;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration _configuration;

        public HangfireService(IRssRepository rssRepository, IArticleRepository articleRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _rssRepository = rssRepository;
            _articleRepository = articleRepository;
            this.httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task AiServiceArticlesGeneration()
        {
            var articles = _articleRepository.GetTableTracking().Where(article => article.Summarized == null).ToList();
            foreach (var article in articles)
            {
                try
                {
                    var client = httpClientFactory.CreateClient();
                    var response = await client.PostAsJsonAsync(_configuration["AiServiceApi:Url"], new { link = article.Link });
                    var result = await response.Content.ReadAsStringAsync();

                    dynamic articleResult = JsonConvert.DeserializeObject(result)!;

                    article.Summarized = articleResult?.summary;
                    article.Image = articleResult?.img;
                    article.Category = articleResult?.category;
                    await _articleRepository.UpdateAsync(article);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }

        public async Task CreateNewArticlesFromRsses()
        {
            var rsses = await _rssRepository.GetTableNoTracking().ToListAsync();
            foreach (var rss in rsses)
            {
                try
                {
                    var feed = await FeedReader.ReadAsync(rss.Link);
                    var existingArticle = await _articleRepository.GetTableNoTracking()
                                                                 .Distinct()
                                                                 .Where(x => x.RSSId == rss.Id)
                                                                 .Select(x => new { Url = x.Link })
                                                                 .ToListAsync();

                    var articlesAdded = feed.Items.Where(article => !existingArticle
                                                           .Any(existing => existing.Url == article.Link))
                                        .Select((article) => new Article
                                        {
                                            Description = article.Description,
                                            Title = article.Title,
                                            Link = article.Link,
                                            CreatedAt = article.PublishingDate ?? DateTime.UtcNow.AddHours(-1),
                                            RSSId = rss.Id,
                                        }).ToList();

                    if (articlesAdded != null)
                    {
                        await _articleRepository.AddRangeAsync(articlesAdded);
                    }
                }
                catch (Exception ex)
                {
                    // handle logger 
                    continue;
                }
            }
        }

        public async Task DeleteOldRssArticle() // Deletes articles older than 14 day
        {
            var articles = await _articleRepository.GetTableNoTracking().ToListAsync();
            var articlesToDelete = articles.Where(x => x.CreatedAt < DateTime.UtcNow.AddDays(-5));
            if (articlesToDelete != null)
            {
                await _articleRepository.DeleteRange(articlesToDelete);
            }
        }


        // Ai model service
        public async Task GenerateClusters()
        {
            try
            {
                var articles = _articleRepository.GetTableNoTracking().Where(x => x.Summarized != null).ToList();
                var articlesToCluster = articles.Select(x => new { x.Id, x.Summarized }).ToList();

                var client = httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(_configuration["AiServiceApi:ClusteringUrl"], articlesToCluster);
                var result = await response.Content.ReadAsStringAsync();

                dynamic clusterResult = JsonConvert.DeserializeObject(result)!;

                foreach (var article in articles)
                {
                    article.ClusterLabel = clusterResult[article.Id.ToString()];
                    await _articleRepository.UpdateAsync(article);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}