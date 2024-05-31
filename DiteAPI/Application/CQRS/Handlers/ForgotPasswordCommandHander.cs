using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class ForgotPasswordCommandHander : IRequestHandler<ResetPasswordCommand, BaseResponse>
    {
        public Task<BaseResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
