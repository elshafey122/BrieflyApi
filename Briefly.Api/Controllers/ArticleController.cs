using Briefly.Core.Features.Article.Commands.Model;
using Briefly.Core.Features.Article.Queires.Model;
using Briefly.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.ApiRoutingData;

namespace Briefly.Api.Controllers
{
    [Authorize]
    public class ArticleController : BaseController
    {

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet(Routes.ArticleRouting.GetRssArticle)]
        public async Task<IActionResult> GetRssArticleById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GerArticleByldQuery { ArticleId = id }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet(Routes.ArticleRouting.GetAllRssArticles)]
        public async Task<IActionResult> GetAllRssArticles([FromQuery] int Rssid, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRssArticlesQuery { RssId = Rssid, PageNumber = PageNumber, pageSize = PageSize }, cancellationToken);
            return Ok(result);
        }
        
        // start

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost(Routes.ArticleRouting.SaveUserArticle)]
        public async Task<IActionResult> SaveUserArticle([FromRoute] int articleId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SaveUserArticleCommand {UserId = GetUserId() , ArticleId = articleId  }, cancellationToken);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet(Routes.ArticleRouting.GetAllUserSavedArticles)]
        public async Task<IActionResult> GetUserSavedArticles([FromQuery] int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllSavedUserArticles { UserId = GetUserId(), pageNumber = PageNumber, PageSize = PageSize }, cancellationToken);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpDelete(Routes.ArticleRouting.DeleteUserSaveArticle)]
        public async Task<IActionResult> DeleteUserSaveArticle([FromRoute] int articleId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteSaveUserArticleCommand { UserId = GetUserId(), ArticleId = articleId }, cancellationToken);
            return Ok(result);
        }

        // end

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost(Routes.ArticleRouting.AddLikeArticle)]
        public async Task<IActionResult> AddLikeArticle([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddLikeArticleCommand { ArticleId = id }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost(Routes.ArticleRouting.DeleteLikeArticle)]
        public async Task<IActionResult> DeleteLikeArticle([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletelikeArticleCommand { ArticleId = id }, cancellationToken);
            return Result(result);
        }
    }
}
