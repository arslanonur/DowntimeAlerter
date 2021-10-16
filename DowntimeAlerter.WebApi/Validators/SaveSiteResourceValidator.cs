using DowntimeAlerter.WebApi.DTO;
using FluentValidation;

namespace DowntimeAlerter.WebApi.Validators
{
    public class SaveSiteResourceValidator : AbstractValidator<SiteDTO>
    {
        public SaveSiteResourceValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}