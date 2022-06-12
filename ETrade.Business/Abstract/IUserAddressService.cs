using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Abstract
{
    public interface IUserAddressService
    {
        IResult Add(int userId, int addressId);
        IResult Add(UserAddress userAddress);
        IResult Add(Address address, int userId);
        IResult Update(UserAddress userAddress);
        IResult Delete(int userAddressId);
        IResult HardDelete(UserAddress userAddress);

        IDataResult<ObjectDto<UserAddress>> GetById(int userAddressId);

        IDataResult<ObjectQueryableDto<UserAddress>> GetAll();

        IDataResult<ObjectQueryableDto<UserAddress>> GetAllActive();
        IDataResult<ObjectQueryableDto<UserAddress>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<UserAddress>> GetAllActiveAndNonDeleted();

        IDataResult<ObjectQueryableDto<Address>> GetAddressesByUserId(int userId);
        IDataResult<ObjectQueryableDto<Address>> GetActiveAddressesByUserId(int userId);
        IDataResult<ObjectQueryableDto<Address>> GetNonDeltedAddressesByUserId(int userId);
        IDataResult<ObjectQueryableDto<Address>> GetNonDeletedAndActiveAddressesByUserId(int userId);
    }
}
