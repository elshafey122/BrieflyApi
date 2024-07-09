using Briefly.Core.Features.CommentsArticle.Queires.ViewModel;
using Briefly.Data.Entities;

namespace Briefly.Core.Mapping.Auth
{
    public partial class AuthMapping : Profile
    {
        public AuthMapping()
        {
            CreateMap<Comment, GetAllCommentsDto>()
                .ForMember(des=>des.UserName,opt=>opt.MapFrom(src=>src.User)).ReverseMap();
        }
    }
}
