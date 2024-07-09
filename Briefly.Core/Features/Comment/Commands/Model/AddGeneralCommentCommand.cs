﻿namespace Briefly.Core.Features.Comment.Commands.Model
{
    public class AddGeneralCommentCommand:IRequest<Response<string>>
    {
        public int userId { get; set; }
        public string text { get; set; }
        public int articleId { get; set; }

    }
}
