using FluentValidation;
using Linnworks.Core.Application.Models;

namespace Linnworks.Web.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.OrderedAt).NotNull();
            RuleFor(v => v.OrderPriorityId).GreaterThan(0);
            RuleFor(v => v.OrderPrioritySymbol)
                .MaximumLength(3)
                .NotEmpty();
        }
    }
}
