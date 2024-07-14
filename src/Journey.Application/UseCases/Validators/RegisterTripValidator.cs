using FluentValidation;
using Journey.Communication.Requests;

namespace Journey.Application.UseCases.Validators;
public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
{
    public RegisterTripValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name is Null or Empty");
        RuleFor(request => request.StartDate).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Start Date is with wrong value");
        RuleFor(request => request).Must(request => request.EndDate >= request.StartDate).WithMessage("Start Date is smaller than End Date");
    }
}
