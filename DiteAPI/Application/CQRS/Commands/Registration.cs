using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class Registration : IRequest<BaseResponse<string>>
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string MiddleName { get; set; }
        public Gender UserGender { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }  
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } 
    }

    public class RegistrationValidator : AbstractValidator<Registration>
    {
        private readonly DataDBContext _dbContext;
        public RegistrationValidator(DataDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.FirstName).NotNull()
                .NotEmpty().WithMessage("Firstname is required.")
                .MaximumLength(100).WithMessage("Username must not be greater than 100 characters.");

            RuleFor(x => x.LastName).NotNull()
                .NotEmpty().WithMessage("Lastname is required.")
                .MaximumLength(100).WithMessage("Username must not be greater than 100 characters.");

            RuleFor(x => x.MiddleName).NotNull()
                .NotEmpty().WithMessage("Middlename is required.")
                .MaximumLength(100).WithMessage("Username must not be greater than 100 characters.");

            RuleFor(x => x.UserGender).IsInEnum();

            RuleFor(x => x.Email).NotNull()
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address.")
                .MustAsync(BeUniqueEmail).WithMessage("Email address is already taken.");

            RuleFor(x => x.UserName).NotNull().NotEmpty()
                .Length(3, 20).WithMessage("Username must be between 3 and 20 characters.")
                .Must(UserName => UserName.ToLowerInvariant() != "admin")
                .Must(UserName => UserName.ToLowerInvariant() != "facilitator")
                .MustAsync(BeUniqueUsername).WithMessage("Username is already taken.");

            RuleFor(x => x.PhoneNumber).NotNull()
                .NotEmpty().WithMessage("Phone number is required.")
                .MustAsync(BeUniquePhoneNumber).WithMessage("Phone number is already taken.");

            RuleFor(x => x.Password).NotNull()
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("The confirmation password does not match the entered password.");
        }

        // checks if the username is being used by another user
        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return !await _dbContext.GenericUser.AnyAsync(u => u.UserName == username);
        }
        
        // checks if the Email is being used by another user 
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _dbContext.GenericUser.AnyAsync(u => u.Email == email);

        }
        
        // checks if the Phone number is being used by another user
        private async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            return !await _dbContext.GenericUser.AnyAsync(u => u.PhoneNumber == phoneNumber);

        }
    }
}