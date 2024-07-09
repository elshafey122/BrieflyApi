using Briefly.Core.Features.Article.Commands.Model;
using Briefly.Data.Identity;
using Briefly.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace Briefly.Core.Features.Article.Commands.Handler
{
    public class ArticleCommandHandler : ResponseHandler,
                                                        IRequestHandler<AddLikeArticleCommand, Response<string>>,
                                                        IRequestHandler<DeletelikeArticleCommand, Response<string>>,
                                                        IRequestHandler<SaveUserArticleCommand, Response<string>>,
                                                        IRequestHandler<DeleteSaveUserArticleCommand, Response<string>>
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _usermanager;
        public ArticleCommandHandler(IArticleService articleService, UserManager<User> usermanager)
        {
            _articleService = articleService;
            _usermanager = usermanager;
        }

        public async Task<Response<string>> Handle(AddLikeArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _articleService.AddLikeArticle(request.ArticleId);
                if(result== "notfound")
                {
                    return NotFound<string>("Article not found");
                }
                return Success("Like added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to add like to article");
            }
        }

        public async Task<Response<string>> Handle(DeletelikeArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _articleService.DeleteLikeArticle(request.ArticleId);
                if (result == "notfound")
                {
                    return NotFound<string>("Article not found");
                }
                return Success("Deleted like successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to delete like from article");
            }
        }

        public async Task<Response<string>> Handle(SaveUserArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _usermanager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return NotFound<string>("User is not found");
                }
                var result = await _articleService.SaveUserArticle(request.UserId, request.ArticleId);
                if(result== "articleNotFound")
                {
                    return NotFound<string>("Article is not found");
                }
                return Success("Saved article successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to save article");
            }
        }

        public async Task<Response<string>> Handle(DeleteSaveUserArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _usermanager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return NotFound<string>("User is not found");
                }
                var result = await _articleService.DeleteUserArticle(request.UserId, request.ArticleId);
                if (result == "userarticleNotFound")
                {
                    return NotFound<string>("User article is not found");
                }
                return Success("Deleted article successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to delete article");
            }
        }
    }
}
