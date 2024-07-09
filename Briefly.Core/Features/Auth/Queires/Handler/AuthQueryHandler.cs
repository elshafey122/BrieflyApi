using Briefly.Core.Features.Auth.Queires.Model;

namespace Briefly.Core.Features.Auth.Queires.Handler
{
    public class RssQueryHandler : ResponseHandler,
                                                   IRequestHandler<ConfirmEmailQuery, Response<string>>,
                                                   IRequestHandler<ConfirmResetPasswordQuery, Response<string>>,
                                                   IRequestHandler<CheckTokenValidationQuery, Response<string>>
    {
        private readonly IAuthService _authService;

        public RssQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmEmailAsync(request.userId, request.Code);
            if (result == "FailedToConfirmEmail")
            {
                return BadRequest<string>("Failed To Confirm Email");
            }
            return Success<string>(null,"Email Confirmed Successfully");
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmResetPassword(request.Email, request.Code);
            switch (result)
            {
                case "usernotfound":
                    return BadRequest<string>("User is not found");
                    break;
                case "failed":
                    return BadRequest<string>("Code is not correct");
                    break;
                default:
                    return Success<string>(null,"Confirmed successfully");
            }
        }

        public async Task<Response<string>> Handle(CheckTokenValidationQuery request, CancellationToken cancellationToken)
        {
            var result = await _authService.CheckValidationToken(request.Token);
            if (result == false)
                return UnAuthorized<string>("Invalid token");
            return Success<string>("valid token");
        }
    }
}