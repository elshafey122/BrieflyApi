using Briefly.Core.Features.Rss.Commands.Model;

namespace Briefly.Core.Features.Rss.Commands.Handler
{
    public class RssCommandHandler : ResponseHandler,
                                                     IRequestHandler<CreateUserRssCommand, Response<string>>,
                                                     IRequestHandler<CreateRssCommand, Response<string>>,
                                                     IRequestHandler<RssUserSubscribeCommand, Response<string>>,
                                                     IRequestHandler<RssUserUnSubscribeCommand, Response<string>>,
                                                     IRequestHandler<UpdateRssCommand, Response<string>>,
                                                     IRequestHandler<DeleteRssCommand, Response<string>>
    {
        private readonly IRssService _rssService;
        public RssCommandHandler(IRssService rssService)
        {
            _rssService = rssService;
        }

        public async Task<Response<string>> Handle(CreateUserRssCommand request, CancellationToken cancellationToken)
        {
            var result = await _rssService.CreateUserRss(request.UserId, request.RssUrl);
            switch (result)
            {
                case ("Added"):
                    return Created<string>("Rss added successfully");
                    break;
                case ("usersubscribed"):
                    return BadRequest<string>("User subscribed to this rss before");
                    break;
                case ("Usernotfound"):
                    return NotFound<string>("User not found");
                    break;
                default:
                    return BadRequest<string>("Rss is not valid");
                    break;
            };
        }

        public async Task<Response<string>> Handle(CreateRssCommand request, CancellationToken cancellationToken)
        {
            var result = await _rssService.CreateRss(request.RssUrl);
            switch (result)
            {
                case ("Added"):
                    return Created<string>("Rss added successfully");
                    break;
                case ("RssFound"):
                    return BadRequest<string>("Rss is already exists");
                    break;
                default:
                    return BadRequest<string>("Rss is not valid");
                    break;
            };
        }

        public async Task<Response<string>> Handle(RssUserSubscribeCommand request, CancellationToken cancellationToken)
        {
            var result = await _rssService.RssUserSubscribe(request.UserId, request.RssId);
            switch (result)
            {
                case ("usernotfound"):
                    return BadRequest<string>("User not found");
                    break;
                case ("rssnotfound"):
                    return BadRequest<string>("Rss not found");
                    break;
                case ("failed"):
                    return BadRequest<string>("Failed to add subscribtion rss to user");
                    break;
                case ("UserSubscrbedBefore"):
                    return BadRequest<string>("User subscrbed to rss before");
                    break;
                default:
                    return Created<string>("Added user subscription successfully");
            }
        }

        public async Task<Response<string>> Handle(RssUserUnSubscribeCommand request, CancellationToken cancellationToken)
        {
            var result = await _rssService.RssUnUserSubscribe(request.UserId, request.RssId);
            switch (result)
            {
                case ("usernotfound"):
                    return NotFound<string>("User is not found");
                    break;
                case ("rssnotfound"):
                    return NotFound<string>("Rss not found");
                    break;
                case ("failed"):
                    return BadRequest<string>("Failed to remove subscribtion rss from user");
                    break;
                case ("UserNotSubscribed"):
                    return BadRequest<string>("User not subscribed to rss before");
                    break;
                default:
                    return Success<string>(null,"Deleted user rss subscription Successfully");
            }
        }

        public async Task<Response<string>> Handle(UpdateRssCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rss = await _rssService.GetRssById(request.Id);
                if (rss == null)
                {
                    return NotFound<string>("Rss not found");
                }
                rss.Title = request.Title;
                rss.Description = request.Description;
                await _rssService.UpdateRss(rss);
                return Success<string>("Updated rss successfully");
            }
            catch(Exception ex)
            {
                return BadRequest<string>("Failed to update rss");
            }
        }

        public async Task<Response<string>> Handle(DeleteRssCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rss = await _rssService.GetRssById(request.RssId);
                if (rss == null)
                {
                    return NotFound<string>("Rss not found");
                }
                var res = await  _rssService.DeleteRss(rss);
                if (res == "Deleted Successfully")
                {
                    return Success<string>("Rss deleted successfully");
                }
                return BadRequest<string>("Failed to delete rss");
            }
            catch (Exception ex)
            {
                return BadRequest<string>("Failed to delete rss");
            }
        }
    }
}