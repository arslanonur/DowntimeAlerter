using DowntimeAlerter.WebApi.DTO;
using FluentValidation;

namespace DowntimeAlerter.WebApi.Validators
{
    public class SaveUserReourceValidator : AbstractValidator<UserDTO>
    {
        public SaveUserReourceValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}