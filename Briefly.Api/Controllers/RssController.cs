using Briefly.Core.Features.Rss.Commands.Model;
using Briefly.Core.Features.Rss.Queires.Model;
using Briefly.Core.Features.Rss.Queires.ViewModel;
using Briefly.Core.Pagination;
using Briefly.Core.Response;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.ApiRoutingData;
using System.Net;

namespace Briefly.Api.Controllers
{
    [Authorize]
    public class RssController : BaseController
    {
        private readonly IValidator<CreateRssCommand> _createRssValidator;
        private readonly IValidator<RssUserSubscribeCommand> _rssUserSubscribeValidator;
        private readonly IValidator<RssUserUnSubscribeCommand> _rssUserUnSubscribeValidator;
        private readonly IValidator<UpdateRssCommand> _updateRssValidator;

        public RssController(IValidator<CreateRssCommand> createRssValidator,
            IValidator<RssUserSubscribeCommand> rssUserSubscribeValidator,
            IValidator<RssUserUnSubscribeCommand> rssUserUnSubscribeValidator,
            IValidator<UpdateRssCommand> updateRssValidator)
        {
            _createRssValidator = createRssValidator;
            _rssUserSubscribeValidator = rssUserSubscribeValidator;
            _rssUserUnSubscribeValidator = rssUserUnSubscribeValidator;
            _updateRssValidator = updateRssValidator;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPost(Routes.RssRouting.CreateUserRss)]
        public async Task<IActionResult> CreateUserRss([FromQuery] string rssUrl, CancellationToken cancellationToken)
        {
            var validation = await _createRssValidator.ValidateAsync(new CreateRssCommand { RssUrl = rssUrl });
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }

            var result = await _mediator.Send(new CreateUserRssCommand { UserId = GetUserId(), RssUrl = rssUrl }, cancellationToken);
            return Result(result);
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPost(Routes.RssRouting.CreateRssByAdmin)]
        public async Task<IActionResult> CreateRss([FromQuery] string rssUrl, CancellationToken cancellationToken)
        {
            var validation = await _createRssValidator.ValidateAsync(new CreateRssCommand { RssUrl = rssUrl });
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }

            var result = await _mediator.Send(new CreateRssCommand { RssUrl = rssUrl }, cancellationToken);
            return Result(result);
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPut(Routes.RssRouting.UpdateRssByAdmin)]
        public async Task<IActionResult> UpdateRss([FromBody] UpdateRssCommand updateRssCommand, CancellationToken cancellationToken)
        {
            var validation = await _updateRssValidator.ValidateAsync(updateRssCommand);
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }

            var result = await _mediator.Send(updateRssCommand, cancellationToken);
            return Result(result);
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpDelete(Routes.RssRouting.DeleteRssByAdmin)]
        public async Task<IActionResult> DeleteRss([FromRoute] DeleteRssCommand deleteRssCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(deleteRssCommand, cancellationToken);
            return Result(result);
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPost(Routes.RssRouting.RssUserSubscribe)]
        public async Task<IActionResult> RssUserSubscribe([FromRoute] int rssId, CancellationToken cancellationToken)
        {
            var validation = await _rssUserSubscribeValidator.ValidateAsync(new RssUserSubscribeCommand { RssId = rssId });
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }

            var result = await _mediator.Send(new RssUserSubscribeCommand { UserId = GetUserId(), RssId = rssId }, cancellationToken);
            return Result(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost(Routes.RssRouting.RssUserUnSubscribe)]
        public async Task<IActionResult> RssUserUnSubscribe([FromRoute] int rssId, CancellationToken cancellationToken)
        {
            var validation = await _rssUserUnSubscribeValidator.ValidateAsync(new RssUserUnSubscribeCommand { RssId = rssId });
            if (!validation.IsValid)
            {
                var errorMessages = string.Join('-', validation.Errors.Select(x => x.ErrorMessage).ToList());
                return BadRequest(new Response<string>("", HttpStatusCode.BadRequest, errorMessages, false));
            }

            var result = await _mediator.Send(new RssUserUnSubscribeCommand { UserId = GetUserId(), RssId = rssId }, cancellationToken);
            return Result(result);
        }


        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PaginationResponse<SubscribrdRssDto>), StatusCodes.Status200OK)]
        [HttpGet(Routes.RssRouting.SubscribedRss)]
        public async Task<IActionResult> SubscribedRss([FromQuery] int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SubscribedRssQuery { UserId = GetUserId(), PageNumber = PageNumber, PageSize = PageSize }, cancellationToken);
            return Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<List<RssDto>>), StatusCodes.Status200OK)]
        [HttpGet(Routes.RssRouting.GetAll)]
        public async Task<IActionResult> GetAllRss([FromQuery] int PageNumber, int PageSize, string? search, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRssQuery { UserId = GetUserId(), PageNumber = PageNumber, PageSize = PageSize, Search = search }, cancellationToken);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<RssDto>), StatusCodes.Status200OK)]
        [HttpGet(Routes.RssRouting.GetById)]
        public async Task<IActionResult> GetRssById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRssByIdQuery { UserId = GetUserId(), RssId = id }, cancellationToken);
            return Result(result);
        }
    }
}