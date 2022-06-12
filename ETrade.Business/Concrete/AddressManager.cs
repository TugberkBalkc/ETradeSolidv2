using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract;
using ETrade.DataAccess.Abstract.Address;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class AddressManager : IAddressService
    {
        private readonly IAddressQueryRepository _addressQueryRepository;
        private readonly IAddressCommandRepository _addressCommandRepository;

        public AddressManager(IAddressQueryRepository addressQueryRepository,
                              IAddressCommandRepository addressCommandRepository)
        {
            _addressQueryRepository = addressQueryRepository;
            _addressCommandRepository = addressCommandRepository;
        }

        public IResult Add(Address address)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfAddressAddedBefore(address.City, address.District, address.Street, address.PostalCode));
            
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _addressCommandRepository.Add(address);
            _addressCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.AddressAdded, BusinessMessages.AddressCouldNotAdded);
        }
          
        public IResult Delete(int addressId)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfAddressExists(addressId));
            
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _addressQueryRepository.Get(a => a.Id == addressId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _addressCommandRepository.Update(entity);
            _addressCommandRepository.SaveChanges();
          
            return new SuccessfulResult(BusinessMessages.AddressDeleted, BusinessTitles.Successful);
        }


        public IDataResult<ObjectQueryableDto<Address>> GetAddressesByUserId(int userId)
        {
            var result = _addressQueryRepository.GetAddressesByUserId(userId);
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }
        public IDataResult<ObjectQueryableDto<Address>> GetAll()
        {
            var result = _addressQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetAllActive()
        {
            var result = _addressQueryRepository.GetAll(a=>a.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetAllActiveAndNonDeleted()
        {
            var result = _addressQueryRepository.GetAll(a => a.IsActive && !a.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetAllByCityName(string cityName)
        {
            var result = _addressQueryRepository.GetAll(a=>a.City.Contains(cityName));
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetAllNonDeleted()
        {
            var result = _addressQueryRepository.GetAll(a=> !a.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectDto<Address>> GetById(int addressId)
        {
            var result = _addressQueryRepository.Get(a=>a.Id == addressId);
            return CheckObjectReturnValue(result,BusinessMessages.AddressFoundDueToFilter, BusinessMessages.AnyAddressFoundDueToFilter);
        }

        public IDataResult<ObjectDto<Address>> GetByPostalCode(string postalCode)
        {
            var result = _addressQueryRepository.Get(a => a.PostalCode == postalCode);
            return CheckObjectReturnValue(result, BusinessMessages.AddressFoundDueToFilter, BusinessMessages.AnyAddressFoundDueToFilter);
        }

        public IResult HardDelete(Address address)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfAddressExists(address.Id));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _addressQueryRepository.Get(a => a.Id == address.Id);

            _addressCommandRepository.HardDelete(entity);
            _addressCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.AddressHardDeleted, BusinessTitles.Successful);
        }

        public IResult Update(Address address)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfAddressExists(address.Id));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _addressCommandRepository.Update(address);
            _addressCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.AddressUpdated, BusinessMessages.AddressCouldNotUpdated);
        }




        //Business Utilities

        private IDataResult<ObjectDto<Address>> CheckObjectReturnValue(Address address,string successMessage, string unSuccessMessage)
        {
           
            if (address != null)
            {
                ObjectDto<Address> objectDto = new ObjectDto<Address>
                {
                    Entity = address,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<Address>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<Address>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<Address>> CheckObjectsReturnValue(IQueryable<Address> addresses, string successMessage, string unSuccessMessage)
        {
            if (addresses.Count()>-1)
            {
                ObjectQueryableDto<Address> objectQueryableDto = new ObjectQueryableDto<Address>
                {
                    Entities = addresses,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<Address>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<Address>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules

        public IResult CheckIfAddressAddedBefore(string city, string district, string street, string postalCode)
        {
            bool status = false;
            var addresses = this.GetAll();
            foreach (var item in addresses.Data.Entities)
            {
                if (item.City.Trim().ToLower() == city.Trim().ToLower() && item.District.Trim().ToLower() == district.Trim().ToLower() &&
                    item.Street.Trim().ToLower() == street.Trim().ToLower() && item.PostalCode.Trim().ToLower() == postalCode)
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.AddressExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        public IResult CheckIfAddressExists(int addressId)
        {
            var addresses = _addressQueryRepository.GetAll();
            foreach (var item in addresses)
            {
                if (item.Id == addressId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.AddressNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfAddressNull(Address address)
        {
            return address == null
                ? new UnSuccessfulResult(BusinessMessages.AddressNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

    }
}
