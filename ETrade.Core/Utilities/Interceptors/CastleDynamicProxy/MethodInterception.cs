using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Interceptors.CastleDynamicProxy
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation İnvocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception exception) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnSuccess(IInvocation invocation) { }


        public override void Intercept(IInvocation invocation)
        {
            var succesStatus = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (System.Exception exception)
            {
                succesStatus = false;
                OnException(invocation, exception);
                throw;
            }
            finally
            {
                if (succesStatus == true)
                {
                    OnSuccess(invocation);
                }
            }

            OnAfter(invocation);
        }

    }
}
