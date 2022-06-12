using ETrade.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name Should Not Be Empty");
            RuleFor(c => c.Name).MinimumLength(3).WithMessage("Name Should Be Grated Than 3 Characters");
            RuleFor(c => c.Detail).NotEmpty().WithMessage("Detail Should Not Be Empty");
            RuleFor(c => c.Detail).MinimumLength(3).WithMessage("Detail Should Be Grated Than 3 Characters");
        }
    }
}
