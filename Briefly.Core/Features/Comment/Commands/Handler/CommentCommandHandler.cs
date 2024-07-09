using Briefly.Core.Features.Comment.Commands.Model;

namespace Briefly.Core.Features.Comment.Commands.Handler
{
    public class CommentCommandHandler : ResponseHandler,  IRequestHandler<AddGeneralCommentCommand, Response<string>>,
                                                           IRequestHandler<AddLocalCommentCommand, Response<string>>,
                                                           IRequestHandler<AddLikeCommentCommand, Response<string>>,
                                                           IRequestHandler<EditCommentCommand, Response<string>>,
                                                           IRequestHandler<DeleteCommentCommand, Response<string>>,
                                                           IRequestHandler<DeleteLikeCommentCommand, Response<string>>
{
    private readonly ICommentService  _commentService;
        public CommentCommandHandler(IRssService rssService, ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<Response<string>> Handle(AddGeneralCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.AddGeneralCommentArticle(request.userId, request.text, request.articleId);
                if (result == "ArticleNotFound") 
                {
                    return NotFound<string>("Article not found");
                }
                return Success<string>("Comment added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to add comment");
            }
        }

        public async Task<Response<string>> Handle(AddLocalCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.AddLocalCommentArticle(request.userId, request.text,
                                                                         request.articleId , request.parentcommentId);
                if(result== "parentcommentid")
                {
                    return NotFound<string>("Parent comment not found");
                }
                else if (result == "ArticleNotFound")
                {
                    return NotFound<string>("Article not found");
                }
                return Success<string>("Comment added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to add comment");
            }
        }

        public async Task<Response<string>> Handle(AddLikeCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.AddLikeCommentArticle(request.commentId);
                if (result == "CommentNotFound")
                {
                    return NotFound<string>("Comment is not found");
                }
                return Success<string>("Like added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to add like to comment");
            }
        }

        public async Task<Response<string>> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.EditCommentArticle(request.userId,request.updatedtext,request.CommentId);
                if (result == "commentnotfound")
                {
                    return NotFound<string>("Comment is not found");
                }
                else if (result == "unauthorizeduser")
                {
                    return BadRequest<string>("User is not owner of comment");
                }
                return Success<string>("Edited comment successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to edit comment");
            }
        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.DeleteCommentArticle(request.UserID , request.CommentId);
                if (result == "commentnotfound")
                {
                    return NotFound<string>("Comment is not found");
                }

                else if(result== "unauthorizeduser")
                {
                    return BadRequest<string>("User is not owner of comment");
                }

                return Success<string>("Deleted comment successfully");
            }
            catch (Exception ex)
            {

                return BadRequest<string>("Failed to delete comment");
            }
        }
         

        public async Task<Response<string>> Handle(DeleteLikeCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commentService.DeleteLikeCommentArticle(request.CommentId);
                if (result == "commentnotfound")
                {
                    return NotFound<string>("Comment is not found");
                }
                return Success<string>("Deleted like successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to delete like");
            }
        }
    }
}