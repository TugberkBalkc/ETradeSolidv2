using Castle.DynamicProxy;
using ETrade.Core.Aspects.Exception;
using ETrade.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Interceptors.CastleDynamicProxy
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
          
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
//"CategoryId" : 2,
//         "Name" : "Rtx2060ti",
//         "Detail" : "Rtx2060ti, 8Gb Ram, 6000MHz Core Speed, Ray Tracing",
//         "StockAmount" : 15,
//         "UnitPrice" : 13000