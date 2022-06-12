using ETrade.Core.Utilities.Interceptors.CastleDynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ETrade.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using ETrade.Core.Extensions;

namespace ETrade.Business.BusinessAspects.CastleDynamicProxy
{
    public class SecuredOperationAspect : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperationAspect(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception("User Has No Authoity");
        }
    }
}

        