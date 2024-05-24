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
#nullable disable
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
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

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty().WithMessage("Firstname is required.")
                .MaximumLength(100).WithMessage("Username must not be greater than 100 characters.");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty().WithMessage("Lastname is required.")
                .MaximumLength(100).WithMessage("Username must not be greater than 100 characters.");

            RuleFor(x => x.MiddleName)
                .NotNull()
                .NotEmpty().WithMessage("Middlename is required.")
                .MaximumLength(100).WithMessage("Username must not be greater than 100 characters.");
              
            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .NotEmpty().WithMessage("Date of birth is required.");

            RuleFor(x => x.UserGender)
                .IsInEnum();

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address.")
                .Must(BeUniqueEmail).WithMessage("Email address is already taken.");

            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty().WithMessage("Firstname is required.")
                .Length(3, 20).WithMessage("Username must be between 3 and 20 characters.")
                .Must(UserName => UserName.ToLowerInvariant() != "admin").WithMessage("This Username is unavailable for use")
                .Must(UserName => UserName.ToLowerInvariant() != "facilitator").WithMessage("This name is unavailable for use")
                .Must(BeUniqueUsername).WithMessage("Username is already taken.");

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(BeUniquePhoneNumber).WithMessage("Phone number is already taken.");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty().WithMessage("Password is required.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!#%*?&])[A-Za-z\d@$!#%*?&]{8,}$").WithMessage("Invalid password format. Your password must be at least 8 characters long and include at least one digit, one lowercase letter, one uppercase letter, one special character, and one unique character."); 
;
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("The confirmation password does not match the entered password.");
        }

        // checks if the username is being used by another user
        private bool BeUniqueUsername(string username)
        {
            return ! _dbContext.GenericUser.Any(u => u.UserName == username);
        }
        
        // checks if the Email is being used by another user 
        private bool BeUniqueEmail(string email)
        {
            return ! _dbContext.GenericUser.Any(u => u.Email == email);

        }
        
        // checks if the Phone number is being used by another user
        private bool BeUniquePhoneNumber(string phoneNumber)
        {
            return ! _dbContext.GenericUser.Any(u => u.PhoneNumber == phoneNumber);
        }
    }
}