using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.Core.Utilities.Security;
using ETrade.DataAccess.Abstract.UserAddress;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class UserAddressManager : IUserAddressService
    {
        private readonly IUserAddressQueryRepository _userAddressQueryRepository;
        private readonly IUserAddressCommandRepository _userAddressCommandRepository;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        public UserAddressManager(IUserAddressQueryRepository userAddressQueryRepository,
                              IUserAddressCommandRepository userAddressCommandRepository,
                              IUserService userService,
                              IAddressService addressService)
        {
            _userAddressQueryRepository = userAddressQueryRepository;
            _userAddressCommandRepository = userAddressCommandRepository;
            _userService = userService;
            _addressService = addressService;
        }

        public IResult Add(Address address, int userId)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (_userService.CheckIfUserExists(userId),
                 _addressService.CheckIfAddressAddedBefore(address.City, address.District, address.Street, address.PostalCode),
                 CheckIfUserAddressAddedBefore(userId, address.Id));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            _addressService.Add(address);

            UserAddress userAddress = new UserAddress
            {
                AddressId = address.Id,
                UserId = userId
            };

            var result = _userAddressCommandRepository.Add(userAddress);
            _userAddressCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.UserAddressAdded, BusinessMessages.UserAddressCouldNotAdded);
        }

        public IResult Add(int userId, int addressId)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (_userService.CheckIfUserExists(userId),
                 _addressService.CheckIfAddressExists(addressId),
                 CheckIfUserAddressAddedBefore(userId, addressId));

            if (logicResult != null)
            {
                return logicResult;
            }

            UserAddress userAddress = new UserAddress
            {
                AddressId = addressId,
                UserId = userId
            };

            var result = _userAddressCommandRepository.Add(userAddress);
            _userAddressCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.UserAddressAdded, BusinessMessages.UserAddressCouldNotAdded);
        }

        public IResult Add(UserAddress userAddress)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (_userService.CheckIfUserExists(userAddress.UserId),
                 _addressService.CheckIfAddressExists(userAddress.Id),
                 CheckIfUserAddressAddedBefore(userAddress.UserId, userAddress.AddressId));
          
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _userAddressCommandRepository.Add(userAddress);
            _userAddressCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.UserAddressAdded, BusinessMessages.UserAddressCouldNotAdded);
        }

        public IResult Delete(int userAddressId)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfUserAddressExists(userAddressId));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _userAddressQueryRepository.Get(ua => ua.Id == userAddressId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _userAddressCommandRepository.Update(entity);
            _userAddressCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.UserAddressDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetAddressesByUserId(int userId)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (_userService.CheckIfUserExists(userId));
            if (logicResult != null)
            {
                return new UnSuccessfulDataResult<ObjectQueryableDto<Address>>(logicResult.Message, logicResult.Title);
            }
            var result = _addressService.GetAddressesByUserId(userId).Data.Entities;
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetActiveAddressesByUserId(int userId)
        {
            var result = _addressService.GetAddressesByUserId(userId).Data.Entities.Where(a=>a.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetNonDeltedAddressesByUserId(int userId)
        {
            var result = _addressService.GetAddressesByUserId(userId).Data.Entities.Where(a => !a.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Address>> GetNonDeletedAndActiveAddressesByUserId(int userId)
        {
            var result = _addressService.GetAddressesByUserId(userId).Data.Entities.Where(a => a.IsActive && !a.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.AddressesFoundDueToFilter, BusinessMessages.AnyAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<UserAddress>> GetAll()
        {
            var result = _userAddressQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.UserAddressesFoundDueToFilter, BusinessMessages.AnyUserAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<UserAddress>> GetAllActive()
        {
            var result = _userAddressQueryRepository.GetAll(ua=>ua.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.UserAddressesFoundDueToFilter, BusinessMessages.AnyUserAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<UserAddress>> GetAllActiveAndNonDeleted()
        {
            var result = _userAddressQueryRepository.GetAll(ua=> ua.IsActive && !ua.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.UserAddressesFoundDueToFilter, BusinessMessages.AnyUserAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<UserAddress>> GetAllNonDeleted()
        {
            var result = _userAddressQueryRepository.GetAll(ua => !ua.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.UserAddressesFoundDueToFilter, BusinessMessages.AnyUserAddressesFoundDueToFilter);
        }

        public IDataResult<ObjectDto<UserAddress>> GetById(int userAddressId)
        {
            var result = _userAddressQueryRepository.Get(a => a.Id == userAddressId);
            return CheckObjectReturnValue(result, BusinessMessages.UserAddressFoundDueToFilter, BusinessMessages.AnyUserAddressFoundDueToFilter);
        }

        public IResult HardDelete(UserAddress userAddress)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfUserAddressExists(userAddress.Id));
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _userAddressQueryRepository.Get(a => a.Id == userAddress.Id);

            _userAddressCommandRepository.HardDelete(entity);
            _userAddressCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.UserAddressHardDeleted, BusinessTitles.Successful);
        }

        public IResult Update(UserAddress userAddress)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfUserAddressExists(userAddress.Id));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _userAddressCommandRepository.Update(userAddress);
            _userAddressCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.AddressUpdated, BusinessMessages.AddressCouldNotUpdated);
        }




        //Business Utilities

        private IDataResult<ObjectDto<UserAddress>> CheckObjectReturnValue(UserAddress userAddress, string successMessage, string unSuccessMessage)
        {

            if (userAddress != null)
            {
                ObjectDto<UserAddress> objectDto = new ObjectDto<UserAddress>
                {
                    Entity = userAddress,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<UserAddress>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<UserAddress>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<UserAddress>> CheckObjectsReturnValue(IQueryable<UserAddress> userAddresses, string successMessage, string unSuccessMessage)
        {
            if (userAddresses.Count() > -1)
            {
                ObjectQueryableDto<UserAddress> objectQueryableDto = new ObjectQueryableDto<UserAddress>
                {
                    Entities = userAddresses,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<UserAddress>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<UserAddress>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<T>> CheckObjectsReturnValue<T>(IQueryable<T> entities, string successMessage, string unSuccessMessage)
        where T : class, new()
        {
            if (entities.Count() > -1)
            {
                ObjectQueryableDto<T> objectQueryableDto = new ObjectQueryableDto<T>
                {
                    Entities = entities,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<T>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<T>>(unSuccessMessage, BusinessTitles.Warning);
        }



        //Business Rules

        private IResult CheckIfUserAddressAddedBefore(int userId, int addressId)
        {
            bool status = false;
            var userAddresses = this.GetAll();
            foreach (var item in userAddresses.Data.Entities)
            {
                if (item.UserId == userId && item.AddressId == addressId)
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.UserAddressExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        private IResult CheckIfUserAddressExists(int userAddressId)
        {
            var userAddresses = _userAddressQueryRepository.GetAll();
            foreach (var item in userAddresses)
            {
                if (item.Id == userAddressId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.UserAddressNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfUserAddressNull(UserAddress userAddress)
        {
            return userAddress == null
                ? new UnSuccessfulResult(BusinessMessages.UserAddressNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
    }
}
