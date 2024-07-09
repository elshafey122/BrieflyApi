namespace Briefly.Service.implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentsRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;

        public CommentService(ICommentsRepository commentRepository, IArticleRepository articleRepository)
        {
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
        }

        public async Task<string> AddGeneralCommentArticle(int userId, string text, int articleId)
        {
            var article = await _articleRepository.GetByIdAsync(articleId);
            if (article == null)
            {
                return "ArticleNotFound";
            }
            var comment = new Comment()
            {
                Text = text,
                PostedDate = DateTime.Now,
                ArticleId = articleId,
                UserId = userId,
            };
            await _commentRepository.AddAsync(comment);

            article.CommentsNumber += 1;
            await _articleRepository.UpdateAsync(article);

            return "Success";
        }

        public async Task<string> AddLocalCommentArticle(int userId, string text, int articleid, int ParentCommentId)
        {
            var article = await _articleRepository.GetByIdAsync(articleid);
            if (article == null)
            {
                return "ArticleNotFound";
            }

            var parentcommnent = await _commentRepository.GetByIdAsync(ParentCommentId);

            if (parentcommnent == null)
            {
                return "ParentCommentNotFound";
            }

            var comment = new Comment()
            {
                Text = text,
                PostedDate = DateTime.Now,
                ArticleId = articleid,
                ParentCommentId = ParentCommentId,
                UserId = userId,
            };

            await _commentRepository.AddAsync(comment);

            article.CommentsNumber += 1;
            await _articleRepository.UpdateAsync(article);

            return "Success";
        }

        public async Task<string> AddLikeCommentArticle(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return "CommentNotFound";
            }
            comment.Likes += 1;
            await _commentRepository.UpdateAsync(comment);
            return "success";
        }

        public async Task<string> EditCommentArticle(int userId, string updatedtext, int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return "commentnotfound";
            }
            if (comment.UserId != userId)
            {
                return "unauthorizeduser";
            }
            comment.Text = updatedtext;
            comment.PostedDate = DateTime.Now;
            await _commentRepository.UpdateAsync(comment);
            return "success";
        }

        public async Task<string> DeleteCommentArticle(string userId, int commentId)
        {
            var userid = int.Parse(userId);
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return "commentnotfound";
            }
            if (comment.UserId != userid)
            {
                return "unauthorizeduser";
            }

            var childComments = await _commentRepository.GetTableTracking().Include(x => x.Article)
                                      .Where(x => x.ParentCommentId == commentId)
                                      .ToListAsync();
            if (childComments != null)
            {
                await _commentRepository.DeleteRange(childComments);
            }
            await _commentRepository.Delete(comment);

            var articleId = comment.ArticleId;
            var article = await _articleRepository.GetByIdAsync(articleId);
            if (article.CommentsNumber > 0)
            {
                article.CommentsNumber -= 1;
                await _articleRepository.UpdateAsync(article);
            }

            return "success";
        }

        public async Task<string> DeleteLikeCommentArticle(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return "commentnotfound";
            }
            if (comment.Likes > 0)
            {
                comment.Likes--;
            }
            await _commentRepository.UpdateAsync(comment);
            return "success";
        }


        public async Task<List<Comment>> GetAllCommentsArticle(int articleId)
        {
            var article = await _articleRepository.GetByIdAsync(articleId);     //var article = await _articleRepository.GetByIdAsync(ArticleId);
            if (article == null)
            {
                return null;
            }

            var allcomments = await _commentRepository.GetTableNoTracking()
                                                      .Include(x => x.User)
                                                      .OrderBy(x => x.PostedDate)
                                                      .Where(x => x.ArticleId == articleId)
                                                      .ToListAsync();

            var outputcomments = new List<Comment>();

            var commentDictionary = allcomments.ToDictionary(c => c.Id);  //id=7 what//id=15 amazing//id=16 means

            foreach (var comment in allcomments)
            {
                if (comment.ParentCommentId != null)
                {

                    if (commentDictionary.TryGetValue(comment.ParentCommentId.Value, out var parentcomment))
                    {
                        if (parentcomment.Replies == null)
                        {
                            parentcomment.Replies = new List<Comment>();
                        }
                        parentcomment.Replies.Add(comment);
                    }
                }
                else
                {
                    outputcomments.Add(comment);
                }
            }
            return outputcomments;
        }
    }
}
