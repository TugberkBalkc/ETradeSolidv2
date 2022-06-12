using ETrade.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.ValidationRules.FluentValidation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.AddressId).NotEmpty().WithMessage("Address Should Not Be Emty");
            RuleFor(o => o.PaymentId).NotEmpty().WithMessage("Payment Type Should Not Be Empty");
            RuleFor(o => o.UserId).NotEmpty().WithMessage("User Should Not Be Empty");
        }
    }
}
