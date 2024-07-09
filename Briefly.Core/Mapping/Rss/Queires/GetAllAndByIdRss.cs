using AutoMapper;
using Briefly.Core.Features.Rss.Queires.ViewModel;
using Briefly.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefly.Core.Mapping.Rss
{
    public partial class RssMapping 
    {
        public void GetAllAndByIdRss()
        {
            CreateMap<RSS, RssDto>().ReverseMap();
        }
    }
}
