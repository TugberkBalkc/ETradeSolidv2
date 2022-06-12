using ETrade.Business.Abstract;
using ETrade.Business.Abstract.AuthenticationAndAuthorization;
using ETrade.Business.Concrete;
using ETrade.Business.Concrete.AuthenticationAndAuthorization;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Security;
using ETrade.Core.Utilities.Security.JsonWebToken;
using ETrade.DataAccess.Abstract;
using ETrade.DataAccess.Abstract.Address;
using ETrade.DataAccess.Abstract.Category;
using ETrade.DataAccess.Abstract.OperationClaim;
using ETrade.DataAccess.Abstract.Order;
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
using ETrade.DataAccess.Concrete.EntityFramework.PaymentType;
using ETrade.DataAccess.Concrete.EntityFramework.Product;
using ETrade.DataAccess.Concrete.EntityFramework.Role;
using ETrade.DataAccess.Concrete.EntityFramework.User;
using ETrade.DataAccess.Concrete.EntityFramework.UserAddress;
using ETrade.DataAccess.Concrete.EntityFramework.UserOperationClaim;
using ETrade.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using System;

namespace ETrade.ConsoleTestUI
{
    class Program
    {
        static void Main(string[] args)
        {
            

            IOperationClaimQueryRepository operationClaimQueryRepository = new EfOperationClaimQueryRepository(new ETradeDataContext());
            IOperationClaimCommandRepository operationClaimCommandRepository = new EfOperationClaimCommandRepository(new ETradeDataContext());

            IOperationClaimService operationClaimService = new OperationClaimManager(operationClaimQueryRepository, operationClaimCommandRepository);



            IRoleOperationClaimQueryRepository roleOperationClaimQueryRepository = new EfRoleOperationClaimQueryRepository(new ETradeDataContext());
            IRoleOperationClaimCommandRepository roleOperationClaimCommandRepository = new EfRoleOperationClaimCommandRepository(new ETradeDataContext());

            IRoleQueryRepository roleQueryRepository = new EfRoleQueryRepository(new ETradeDataContext());
            IRoleCommandRepository roleCommandRepository = new EfRoleCommandRepository(new ETradeDataContext());

            IRoleService roleService = new RoleManager(roleQueryRepository, roleCommandRepository);

            IRoleOperationClaimService roleOperationClaimService = new RoleOperationClaimManager
                (roleOperationClaimQueryRepository, roleOperationClaimCommandRepository, operationClaimService, roleService);

            IUserQueryRepository userQueryRepository = new EfUserQueryRepository(new ETradeDataContext());
            IUserCommandRepository userCommandRepository = new EfUserCommandRepository(new ETradeDataContext());
            IUserService userService = new UserManager(userQueryRepository, userCommandRepository, roleOperationClaimService);



            Category category = new Category();
            category.Id = 1;
            category.Name = "Electronic";

            Category category1 = new Category();
            category.Id = 2;
            category.Name = "Used";

            Category category2 = new Category();
            category.Id = 3;
            category.Name = "New";





        }
    }
}
