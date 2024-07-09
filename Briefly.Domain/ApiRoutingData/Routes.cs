namespace SchoolProject.Data.ApiRoutingData
{
    public static class Routes
    {
        public static class AuthRouting
        {
            public const string Register = "Register";
            public const string Login = "Login";
            public const string ConfirmEmail = "ConfirmEmail";
            public const string SendResetPassword = "SendResetPassword";
            public const string ConfirmResetPassword = "ConfirmResetPassword";
            public const string ResetPassword = "ResetPassword";
            public const string GenerateRefreshToken = "GenerateRefreshToken";
            public const string CheckValidationToken = "CheckValidationToken";
            public const string LoginGoogle = "Login-Google";
        }
        public static class RssRouting
        {
            public const string GetAll = "GetAll";
            public const string GetById = "{id}";
            public const string CreateUserRss = "CreateUserRss";
            public const string CreateRssByAdmin = "CreateRssByAdmin";
            public const string UpdateRssByAdmin = "UpdateRssByAdmin";
            public const string DeleteRssByAdmin = "DeleteRssByAdmin/{RssId}";
            public const string RssUserSubscribe = "RssUserSubscribe/{rssId}";
            public const string RssUserUnSubscribe = "RssUserUnSubscribe/{rssId}";
            public const string SubscribedRss = "SubscribedRss/All";
            public const string GetRssCategories = "GetRssCategories";

        }
        public static class ArticleRouting
        {
            public const string GetRssArticle = "GetRssArticle/{id}"; 
            public const string GetAllRssArticles = "GetAllRssArticles";
            public const string GetAllUserSavedArticles = "GetAllUserSavedArticles";
            public const string SaveUserArticle = "SaveUserArticle/{articleId}";
            public const string DeleteUserSaveArticle = "DeleteUserSaveArticle/{articleId}";

            public const string AddLikeArticle =    "AddLikeArticle/{id}";
            public const string DeleteLikeArticle = "DeleteLikeArticle/{id}";
        }

        public static class CommentsArticleRouting
        {
            public const string AddGeneralCommentArticle = "AddGeneralCommentArticle"; 
            public const string AddLocalCommentArticle = "AddLocalCommentArticle"; 
            public const string AddLikeCommentArticle = "AddLikeCommentArticle/{commentId}"; 
            public const string EditCommentArticle = "EditCommentArticle";

            public const string DeleteCommentArticle = "DeleteCommentArticle/{commentId}";
            public const string DeleteLikeCommentArticle = "DeleteLikeCommentArticle/{commentId}";
            public const string GetAllCommentsArticle = "GetAllCommentsArticle/{articleId}";




        }
    }
}
