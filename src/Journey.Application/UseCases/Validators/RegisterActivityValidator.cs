using FluentValidation;
using Journey.Communication.Requests;

namespace Journey.Application.UseCases.Validators;
public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Activity With No Name");
    }
}
