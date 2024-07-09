using System.Text.Json.Serialization;

namespace Briefly.Core.Features.Rss.Commands.Model
{
    public class RssUserSubscribeCommand:IRequest<Response<string>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public int RssId { get; set; }
    }
}
