using AutoMapper;
using Briefly.Core.Features.CommentsArticle.Queires.ViewModel;
using Briefly.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefly.Core.Mapping.CommentsArticle
{
    public partial class CommentMapping
    {
        public void GetAllCommentsArticle() 
        {
            CreateMap<Comment, GetAllCommentsDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
