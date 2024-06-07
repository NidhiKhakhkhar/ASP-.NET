using FluentValidation;

namespace ApiDemo.Model.UserValidator
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required");
            RuleFor(user =>user.Password).NotEmpty();
            RuleFor(user => user.ConfirmPassword).NotEmpty();
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Age).NotEmpty().InclusiveBetween(12, 120);


        }

    }
}
