namespace Briefly.Service.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllCommentsArticle(int articleId);
        Task<string> AddGeneralCommentArticle(int userId, string text, int articleId);
        Task<string> AddLocalCommentArticle(int userId, string text, int articleId, int parentcommentid);
        Task<string> AddLikeCommentArticle(int commentId);

        Task<string> EditCommentArticle(int userId, string updatedtext, int articleId);

        Task<string> DeleteCommentArticle(string userId, int commentId);
        Task<string> DeleteLikeCommentArticle(int commentId);
    }
}
