
namespace Briefly.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentsRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
