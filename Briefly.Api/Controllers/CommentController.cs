using Briefly.Core.Features.Comment.Commands.Model;
using Briefly.Core.Features.CommentsArticle.Queires.Model;
using Briefly.Core.Response;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.ApiRoutingData;
using System.Net;

namespace Briefly.Api.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        private readonly IValidator<AddGeneralCommentCommand> _addGeneralCommentArticleValidator;
        private readonly IValidator<AddLocalCommentCommand> _addLocalCommentArticleValidator;
        private readonly IValidator<EditCommentCommand> _editCommentArticleValidator;

        public CommentController(IValidator<AddGeneralCommentCommand> addGeneralCommentArticleValidator,
                        IValidator<AddLocalCommentCommand> addLocalCommentArticleValidator,
                        IValidator<EditCommentCommand> editCommentArticleValidator)
        {
            _addGeneralCommentArticleValidator = addGeneralCommentArticleValidator;
            _addLocalCommentArticleValidator = addLocalCommentArticleValidator;
            _editCommentArticleValidator = editCommentArticleValidator;

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPost(Routes.CommentsArticleRouting.AddGeneralCommentArticle)]
        public async Task<IActionResult> AddGeneralCommentArticle([FromQuery] string text,int articleId, CancellationToken cancellationToken)
        {
            var validation = await _addGeneralCommentArticleValidator.ValidateAsync(new AddGeneralCommentCommand { text = text });
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }
            var result = await _mediator.Send(new AddGeneralCommentCommand { userId = int.Parse(GetUserId()), text = text ,articleId = articleId }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPost(Routes.CommentsArticleRouting.AddLocalCommentArticle)]
        public async Task<IActionResult> AddLocalCommentArticle([FromQuery] string text, int articleId , int parentcommentId , CancellationToken cancellationToken)
        {
            var validation = await _addLocalCommentArticleValidator.ValidateAsync(new AddLocalCommentCommand { text = text });
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }
            var result = await _mediator.Send(new AddLocalCommentCommand { userId = int.Parse(GetUserId()), text = text, articleId = articleId ,parentcommentId = parentcommentId }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPost(Routes.CommentsArticleRouting.AddLikeCommentArticle)]
        public async Task<IActionResult> AddLikeCommentArticle([FromRoute] int commentId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddLikeCommentCommand { commentId = commentId }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPut(Routes.CommentsArticleRouting.EditCommentArticle)]
        public async Task<IActionResult> EditCommentArticle([FromQuery] string text , int commentId, CancellationToken cancellationToken)
        {
            var validation = await _editCommentArticleValidator.ValidateAsync(new EditCommentCommand { updatedtext = text });
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }
            var result = await _mediator.Send(new EditCommentCommand { userId = int.Parse(GetUserId()), updatedtext = text, CommentId = commentId }, cancellationToken);
            return Result(result);
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpDelete(Routes.CommentsArticleRouting.DeleteCommentArticle)]
        public async Task<IActionResult> DeleteCommentArticle([FromRoute] int commentId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCommentCommand { UserID = GetUserId() , CommentId = commentId }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost(Routes.CommentsArticleRouting.DeleteLikeCommentArticle)]
        public async Task<IActionResult> DeleteLikeCommentArticle([FromRoute] int commentId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteLikeCommentCommand { CommentId = commentId }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet(Routes.CommentsArticleRouting.GetAllCommentsArticle)]
        public async Task<IActionResult> GetAllCommentsArticle([FromRoute] int articleId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCommentsQuery { articleId  = articleId }, cancellationToken);
            return Result(result);
        }
    }
}