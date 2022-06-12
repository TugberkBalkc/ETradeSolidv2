using ETrade.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.ValidationRules.FluentValidation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(c => c.City).NotEmpty().WithMessage("City Should Not Be Empty");
            RuleFor(c => c.City).MinimumLength(3).WithMessage("City Should Be Grated Than 3 Characters");
            RuleFor(c => c.District).NotEmpty().WithMessage("District Should Not Be Empty");
            RuleFor(c => c.District).MinimumLength(3).WithMessage("District Should Be Grated Than 3 Characters");
            RuleFor(c => c.Street).NotEmpty().WithMessage("Street Should Not Be Empty");
            RuleFor(c => c.Street).MinimumLength(3).WithMessage("Street Should Be Grated Than 3 Characters");
            RuleFor(c => c.PostalCode).NotEmpty().WithMessage("Postal Code Should Not Be Empty");
            RuleFor(c => c.PostalCode).MinimumLength(3).WithMessage("Postal Code Should Be Grated Than 3 Characters");
        }
    }
}
