using ETrade.Core.Entities.Concrete;
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
    public interface IAddressService
    {
        IResult Add(Address address);
        IResult Update(Address address);
        IResult Delete(int addressId);
        IResult HardDelete(Address address);

        IDataResult<ObjectDto<Address>> GetById(int addressId);
        IDataResult<ObjectDto<Address>> GetByPostalCode(string postalCode);

        IDataResult<ObjectQueryableDto<Address>> GetAll();
        IDataResult<ObjectQueryableDto<Address>> GetAllByCityName(string cityName);

        IDataResult<ObjectQueryableDto<Address>> GetAllActive();
        IDataResult<ObjectQueryableDto<Address>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<Address>> GetAllActiveAndNonDeleted();

        IDataResult<ObjectQueryableDto<Address>> GetAddressesByUserId(int userId);


        IResult CheckIfAddressAddedBefore(string city, string district, string street, string postalCode);
        IResult CheckIfAddressExists(int addressId);
    }
}
