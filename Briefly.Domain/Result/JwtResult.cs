namespace Briefly.Data.Result
{
    public class JwtResult
    {
        public string Token { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
    public class RefreshToken
    {
        public string RefreshTokenString { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
