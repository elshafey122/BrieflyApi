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

namespace Briefly.Core.Mapping.Rss
{
    public partial class RssMapping
    {
        public void GetRssSubscribeMapper()
        {
            CreateMap<RSS, SubscribrdRssDto>();
        }
    }
}
