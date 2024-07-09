using System.Text.Json.Serialization;

namespace Briefly.Core.Features.Rss.Commands.Model
{
    public class CreateUserRssCommand:IRequest<Response<string>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? RssUrl { get; set; }
    }

    public class CreateRssCommand : IRequest<Response<string>>
    {
        public string? RssUrl { get; set; }
    }
}
