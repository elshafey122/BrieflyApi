using AutoMapper;
using Briefly.Core.Features.Auth.Commands.Model;
using Briefly.Core.Features.Rss.Queires.ViewModel;
using Briefly.Data.Entities;
using Briefly.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefly.Core.Mapping.Auth
{
    public partial class AuthMapping
    {
        public void RegisterMapper()
        {
            CreateMap<RegisterCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
        }
    }
}
