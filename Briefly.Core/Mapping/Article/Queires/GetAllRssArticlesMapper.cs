using AutoMapper;
using Briefly.Core.Features.Article.Queires.ViewModel;
using Briefly.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefly.Core.Mapping.Articles
{
    public partial class ArticleMapping 
    {
        public void GetAllRssArticlesMapper()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
        }
    }
}