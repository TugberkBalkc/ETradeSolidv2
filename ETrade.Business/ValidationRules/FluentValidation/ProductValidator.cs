using ETrade.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name Should Not Be Empty");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Name Should Be Grated Than 3 Characters");
            RuleFor(p => Convert.ToInt64(p.StockAmount)).GreaterThanOrEqualTo(0).WithMessage("Stock Should Be Greater Than Zero");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("Unit Price Should Be Greater Than Zero");
            RuleFor(p => p.Detail).NotEmpty().WithMessage("Details Should Not Be Emty");
        }
    }
}
