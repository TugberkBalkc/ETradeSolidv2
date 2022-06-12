using Castle.DynamicProxy;
using ETrade.Core.CrossCuttingConcerns.Validation.FluentValidation;
using ETrade.Core.Utilities.Interceptors.CastleDynamicProxy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Aspects.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) throw new System.Exception("The Given Validator Type is not a IValidator");
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation İnvocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = İnvocation.Arguments.Where(e => e.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
