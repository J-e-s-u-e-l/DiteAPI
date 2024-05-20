using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class RegistrationHandler : IRequestHandler<Registration, BaseResponse<string>>
    {
        private readonly DataDBContext _dbContext;
        private UserManager<GenericUser> _userManager;
        private readonly ILogger<Registration> _logger;
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        public RegistrationHandler(
            DataDBContext dbContext,
            UserManager<GenericUser> userManager,
            ILogger<Registration> logger,
            IAccountService accountService,
            IEmailService emailService
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
            _accountService = accountService;
            _emailService = emailService;
        }

        public Task<BaseResponse<string>> Handle(Registration request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
