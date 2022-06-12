using Autofac;
using Autofac.Extras.DynamicProxy;
using ETrade.Business.Abstract;
using ETrade.Business.Abstract.AuthenticationAndAuthorization;
using ETrade.Business.Concrete;
using ETrade.Business.Concrete.AuthenticationAndAuthorization;
using ETrade.Core.Utilities.Interceptors.CastleDynamicProxy;
using ETrade.Core.Utilities.Security;
using ETrade.Core.Utilities.Security.JsonWebToken;
using ETrade.DataAccess.Abstract;
using ETrade.DataAccess.Abstract.Address;
using ETrade.DataAccess.Abstract.Category;
using ETrade.DataAccess.Abstract.OperationClaim;
using ETrade.DataAccess.Abstract.Order;
using ETrade.DataAccess.Abstract.OrderDetail;
using ETrade.DataAccess.Abstract.PaymentType;
using ETrade.DataAccess.Abstract.Product;
using ETrade.DataAccess.Abstract.Role;
using ETrade.DataAccess.Abstract.User;
using ETrade.DataAccess.Abstract.UserAddress;
using ETrade.DataAccess.Abstract.UserOperationClaim;
using ETrade.DataAccess.Concrete.EntityFramework.Address;
using ETrade.DataAccess.Concrete.EntityFramework.Category;
using ETrade.DataAccess.Concrete.EntityFramework.Contexts;
using ETrade.DataAccess.Concrete.EntityFramework.OperationClaim;
using ETrade.DataAccess.Concrete.EntityFramework.Order;
using ETrade.DataAccess.Concrete.EntityFramework.OrderDetail;
using ETrade.DataAccess.Concrete.EntityFramework.PaymentType;
using ETrade.DataAccess.Concrete.EntityFramework.Product;
using ETrade.DataAccess.Concrete.EntityFramework.Role;
using ETrade.DataAccess.Concrete.EntityFramework.User;
using ETrade.DataAccess.Concrete.EntityFramework.UserAddress;
using ETrade.DataAccess.Concrete.EntityFramework.UserOperationClaim;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ETradeDataContext>().As<DbContext>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<AuthorizationManager>().As<IAuthorizationService>();
            builder.RegisterType<AuthenticationManager>().As<IAuthenticationService>();

            builder.RegisterType<AddressManager>().As<IAddressService>();
            builder.RegisterType<EfAddressQueryRepository>().As<IAddressQueryRepository>();
            builder.RegisterType<EfAddressCommandRepository>().As<IAddressCommandRepository>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryQueryRepository>().As<ICategoryQueryRepository>();
            builder.RegisterType<EfCategoryCommandRepository>().As<ICategoryCommandRepository>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimQueryRepository>().As<IOperationClaimQueryRepository>();
            builder.RegisterType<EfOperationClaimCommandRepository>().As<IOperationClaimCommandRepository>();

            builder.RegisterType<OrderDetailManager>().As<IOrderDetailService>();
            builder.RegisterType<EfOrderDetailQueryRepository>().As<IOrderDetailQueryRepository>();
            builder.RegisterType<EfOrderDetailCommandRepository>().As<IOrderDetailCommandRepository>();

            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<EfOrderQueryRepository>().As<IOrderQueryRepository>();
            builder.RegisterType<EfOrderCommandRepository>().As<IOrderCommandRepository>();

            builder.RegisterType<PaymentTypeManager>().As<IPaymentTypeService>();
            builder.RegisterType<EfPaymentTypeQueryRepository>().As<IPaymentTypeQueryRepository>();
            builder.RegisterType<EfPaymentTypeCommandRepository>().As<IPaymentTypeCommandRepository>();

            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductQueryRepository>().As<IProductQueryRepository>();
            builder.RegisterType<EfProductCommandRepository>().As<IProductCommandRepository>();

            builder.RegisterType<RoleOperationClaimManager>().As<IRoleOperationClaimService>();
            builder.RegisterType<EfRoleOperationClaimQueryRepository>().As<IRoleOperationClaimQueryRepository>();
            builder.RegisterType<EfRoleOperationClaimCommandRepository>().As<IRoleOperationClaimCommandRepository>();

            builder.RegisterType<RoleManager>().As<IRoleService>();
            builder.RegisterType<EfRoleQueryRepository>().As<IRoleQueryRepository>();
            builder.RegisterType<EfRoleCommandRepository>().As<IRoleCommandRepository>();
           

            builder.RegisterType<UserAddressManager>().As<IUserAddressService>();
            builder.RegisterType<EfUserQueryRepository>().As<IUserQueryRepository>();
            builder.RegisterType<EfUserCommandRepository>().As<IUserCommandRepository>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserAddressQueryRepository>().As<IUserAddressQueryRepository>();
            builder.RegisterType<EfUserAddressCommandRepository>().As<IUserAddressCommandRepository>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(
                new Castle.DynamicProxy.ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();


        }
    }
}
