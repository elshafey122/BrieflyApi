
namespace Briefly.Infrastructure.Repositories
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        private readonly IRssRepository _rssRepository;
        public ArticleRepository(ApplicationDbContext context, IRssRepository rssRepository) : base(context)
        {
            _rssRepository = rssRepository;
        }

        public async Task<List<ICollection<Article>>> GetAllArticles(int RssId)
        {
            var res = await _rssRepository.GetTableNoTracking()
                                           .Include(x=>x.Articles)
                                           .Where(x=>x.Id==RssId)
                                           .ToListAsync();
            return null;
        }
    }
}
