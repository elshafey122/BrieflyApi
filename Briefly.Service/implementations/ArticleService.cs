namespace Briefly.Service.Implemintations.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IRssRepository _rssRepository;
        private readonly IGenericRepository<SavedArticle> _savaArticle;
        public ArticleService(IRssRepository rssRepository, IArticleRepository articleRepository,
            IGenericRepository<SavedArticle> savaArticle)
        {
            _rssRepository = rssRepository;
            _articleRepository = articleRepository;
            _savaArticle = savaArticle;
        }

        public async Task<string> AddLikeArticle(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return "notfound";
            }
            article.Likes++;
            await _articleRepository.UpdateAsync(article);
            return "success";
        }

        public async Task<string> DeleteLikeArticle(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return "notfound";
            }
            if (article.Likes > 0)
            {
                article.Likes--;
            }
            await _articleRepository.UpdateAsync(article);
            return "success";
        }

        public async Task<string> DeleteUserArticle(string userId, int articleId)
        {
            int userid = int.Parse(userId);
            var savearticle = await _savaArticle.GetTableTracking()
                                                .Where(x => x.UserId == userid && x.ArticleId == articleId)
                                                .FirstOrDefaultAsync();
            if (savearticle == null)
            {
                return "userarticleNotFound";
            }
            await _savaArticle.Delete(savearticle);
            return "success";
        }

        public async Task<(string, List<Article>)> GetAllRssArtricles(int RssId)
        {
            var rss = await _rssRepository.GetByIdAsync(RssId);
            if (rss == null)
            {
                return ("RssNotFound", null);
            }
            var articles = await _articleRepository.GetTableNoTracking().Distinct().Where(x => x.RSSId == RssId && x.Summarized != null).ToListAsync();
            if (articles == null)
            {
                return ("NoContent", null);
            }
            return ("", articles);
        }

        public async Task<(string, List<Article>)> GetAllSaveUserArtricles(int userId)
        {
            var Saveuserarticles = await _savaArticle.GetTableNoTracking()
                                                     .Include(z=>z.User)
                                                     .Where(x => x.UserId == userId)
                                                     .Select(x=> new Article
                                                     {
                                                        Title = x.Article.Title,
                                                        Description = x.Article.Description,
                                                        CreatedAt = x.Article.CreatedAt,
                                                        Image = x.Article.Image,
                                                        Views = x.Article.Views,
                                                        Likes = x.Article.Likes,
                                                        Id = x.Article.Id
                                                     })
                                                     .ToListAsync();

            if(Saveuserarticles == null)
            {
                return ("nodata",null);
            }
            return ("", Saveuserarticles);
        }

        public async Task<(string,Article)> GetRssArticlesById(int ArticleId)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(ArticleId);
                if (article == null)
                {
                    return ("ArticleNotFound", null);
                }
                article.Views+=1;
                await _articleRepository.UpdateAsync(article);

                var RelatedArticles = await _articleRepository.GetTableNoTracking()
                                                              .Where(x=>x.ClusterLabel==article.ClusterLabel && x.Id!= ArticleId)
                                                              .Take(5)
                                                              .ToListAsync();

                if (RelatedArticles != null)
                {
                    article.Clusters = RelatedArticles;
                }

                return ("", article);
            }
            catch (Exception ex)
            {
                return ("Fail", null);
            }
       }

        public async Task<string> SaveUserArticle(string userId, int articleId)
        {
            var article = await _articleRepository.GetByIdAsync(articleId);
            if (article == null)
            {
                return "articleNotFound";
            }
            var savearticle = new SavedArticle()
            {
                ArticleId = articleId,
                UserId = int.Parse(userId)
            };
            await _savaArticle.AddAsync(savearticle);
            return "success";
        }
    }
}