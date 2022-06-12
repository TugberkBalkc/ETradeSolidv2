using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Abstract;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.User;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IRoleOperationClaimService _roleOperationClaimService;

        public UserManager(IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository,
            IRoleOperationClaimService roleOperationClaimService)
        {
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _roleOperationClaimService = roleOperationClaimService;
        }

        public IResult Add(User user)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfUserAddedBefore(user.Email));

            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _userCommandRepository.Add(user);
            _userCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.UserAdded, BusinessMessages.UserCouldNotAdded);
        }

        public IResult Delete(int userId)
        {
            var logicResult =
             BusinessLogicEngine.Run
             (CheckIfUserExists(userId));

            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _userQueryRepository.Get(u => u.Id == userId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _userCommandRepository.Update(entity);
            _userCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.UserDeleted, BusinessTitles.Successful);
        }

        public IResult HardDelete(User user)
        {
            var logicResult =
              BusinessLogicEngine.Run
              (CheckIfUserExists(user.Id));
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _userQueryRepository.Get(u => u.Id == user.Id);

            _userCommandRepository.HardDelete(entity);
            _userCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.UserHardDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<UserDetailsDto>> GetVerifiedUserDetails()
        {
            var result = _userQueryRepository.GetAllUserDetails(u=>u.IsVerificated == true);
            return CheckObjectsReturnValue<UserDetailsDto>(result, BusinessMessages.UserFoundDueToFilter, BusinessTitles.Successful);
        }

        public IDataResult<ObjectDto<UserDetailsDto>> GetUserDetailsByEmail(string email)
        {
            var result = _userQueryRepository.GetUserDetails(u => u.Email == email);
            return CheckObjectReturnValue<UserDetailsDto>(result, BusinessMessages.UserFoundDueToFilter, BusinessTitles.Successful);
        }

        public IDataResult<ObjectDto<User>> GetById(int userId)
        {
            var result = _userQueryRepository.Get(u => u.Id == userId);
            return CheckObjectReturnValue(result, BusinessMessages.UserFoundDueToFilter, BusinessMessages.AnyUserFoundDueToFilter);
        }

        public IDataResult<ObjectDto<UserDetailsDto>> GetUserDetailsById(int userId)
        {
            var user = this.GetById(userId);
            var result = _userQueryRepository.GetUserDetails(u => u.Email == user.Data.Entity.Email);
            return CheckObjectReturnValue<UserDetailsDto>(result, BusinessMessages.UserFoundDueToFilter, BusinessMessages.AnyUserFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<User>> GetUsersActive()
        {
            var result = _userQueryRepository.GetAll(u => u.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<User>> GetUsersNonDeleted()
        {
            var result = _userQueryRepository.GetAll(u => !u.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }


        public IDataResult<ObjectQueryableDto<User>> GetUsersActiveAndNonDeleted()
        {
            var result = _userQueryRepository.GetAll(u => u.IsActive && !u.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }


        public IDataResult<ObjectQueryableDto<OperationClaim>> GetUsersClaimsById(int userId)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfUserExistsInOperationClaims(userId));

            if (logicResult !=null)
            {
                return (IDataResult<ObjectQueryableDto<OperationClaim>>)logicResult;
            }
            var user = this.GetById(userId).Data.Entity;
            var result = _roleOperationClaimService.GetOperationClaimsByRoleId(user.RoleId).Data.Entities.AsQueryable();
            return CheckObjectsReturnValue<OperationClaim>(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveUsersClaimsById(int userId)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfUserExistsInOperationClaims(userId));

            if (logicResult != null)
            {
                return (IDataResult<ObjectQueryableDto<OperationClaim>>)logicResult;
            }
            var user = this.GetById(userId).Data.Entity;
            var result = _roleOperationClaimService.GetOperationClaimsByRoleId(user.RoleId).Data.Entities.AsQueryable().Where(opc=>opc.IsActive);
            return CheckObjectsReturnValue<OperationClaim>(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetNonDeletedUsersClaimsById(int userId)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfUserExistsInOperationClaims(userId));

            if (logicResult != null)
            {
                return (IDataResult<ObjectQueryableDto<OperationClaim>>)logicResult;
            }
            var user = this.GetById(userId).Data.Entity;
            var result = _roleOperationClaimService.GetOperationClaimsByRoleId(user.RoleId).Data.Entities.AsQueryable().Where(opc => !opc.IsDeleted);
            return CheckObjectsReturnValue<OperationClaim>(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveAndNonDeletedUsersClaimsById(int userId)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfUserExistsInOperationClaims(userId));

            if (logicResult != null)
            {
                return (IDataResult<ObjectQueryableDto<OperationClaim>>)logicResult;
            }
            var user = this.GetById(userId).Data.Entity;
            var result = _roleOperationClaimService.GetOperationClaimsByRoleId(user.RoleId).Data.Entities.AsQueryable().Where(opc => opc.IsActive && !opc.IsDeleted);
            return CheckObjectsReturnValue<OperationClaim>(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }
        public IDataResult<ObjectDto<User>> GetUserByEmail(string email)
        {
            var result = _userQueryRepository.Get(u => u.Email == email);
            return CheckObjectReturnValue(result, BusinessMessages.UserFoundDueToFilter, BusinessMessages.AnyUserFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<User>> GetUsers()
        {
            var result = _userQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<UserDetailsDto>> GetUserDetails()
        {
            var result = _userQueryRepository.GetAllUserDetails();
            return CheckObjectsReturnValue<UserDetailsDto>(result, BusinessMessages.UsersFoundDueToFilter, BusinessMessages.AnyUsersFoundDueToFilter);
        }

        public IResult Update(User user)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfUserExists(user.Id));

            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _userCommandRepository.Update(user);
            _userCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.UserUpdated, BusinessMessages.UserCouldNotUpdated);
        }





        //Business Utilities

        private IDataResult<ObjectDto<User>> CheckObjectReturnValue(User user, string successMessage, string unSuccessMessage)
        {

            if (user != null)
            {
                ObjectDto<User> objectDto = new ObjectDto<User>
                {
                    Entity = user,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<User>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<User>>(new ObjectDto<User> { Entity = new User(), ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Error}, unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectDto<T>> CheckObjectReturnValue<T>(T entity, string successMessage, string unSuccessMessage)
            where T : class, new()
        {
            if (entity != null)
            {
                ObjectDto<T> objectDto = new ObjectDto<T>
                {
                    Entity = entity,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<T>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<T>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<User>> CheckObjectsReturnValue(IQueryable<User> users, string successMessage, string unSuccessMessage)
        {
            if (users.Count() > -1)
            {
                ObjectQueryableDto<User> objectQueryableDto = new ObjectQueryableDto<User>
                {
                    Entities = users,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<User>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<User>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<T>> CheckObjectsReturnValue<T>(IQueryable<T> tEntities, string successMessage, string unSuccessMessage)
        where T : class, new()
        {
            if (tEntities.Count() > -1)
            {
                ObjectQueryableDto<T> objectQueryableDto = new ObjectQueryableDto<T>
                {
                    Entities = tEntities,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<T>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<T>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules

        public IResult CheckIfUserAddedBefore(string email)
        {
            bool status = false;
            var users = this.GetUsers();
            foreach (var item in users.Data.Entities)
            {
                if (item.Email.Trim().ToLower() == email.Trim().ToLower())
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.UserExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> CheckIfUserExistsInOperationClaims(string email)
        {
            var users = _userQueryRepository.GetAll();
            foreach (var item in users)
            {
                if (item.Email == email)
                {
                    return new SuccessfulDataResult<ObjectQueryableDto<OperationClaim>>();
                }
            }
            var queryable = new List<OperationClaim>().AsQueryable<OperationClaim>();
            return new UnSuccessfulDataResult<ObjectQueryableDto<OperationClaim>>(new ObjectQueryableDto<OperationClaim> { Entities = queryable, ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.UnSuccessful }, BusinessMessages.UserNotFound, BusinessTitles.Warning);
        }

        public IResult CheckIfUserExists(int userId)
        {
            var users = _userQueryRepository.GetAll();
            foreach (var item in users)
            {
                if (item.Id == userId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.UserNotFound, BusinessTitles.Warning);
        }

        public IResult CheckIfUserExists(string email)
        {
            var users = _userQueryRepository.GetAll();
            foreach (var item in users)
            {
                if (item.Email.Trim().ToLower() == email.Trim().ToLower())
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.UserNotFound, BusinessTitles.Warning);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> CheckIfUserExistsInOperationClaims(int userId)
        {
            var users = _userQueryRepository.GetAll();
            foreach (var item in users)
            {
                if (item.Id == userId)
                {
                    return new SuccessfulDataResult<ObjectQueryableDto<OperationClaim>>();
                }
            }
            var queryable = new List<OperationClaim>().AsQueryable<OperationClaim>(); 
            return new UnSuccessfulDataResult<ObjectQueryableDto<OperationClaim>>(new ObjectQueryableDto<OperationClaim> {Entities = queryable, ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.UnSuccessful },BusinessMessages.UserNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfUserNull(User user)
        {
            return user == null
                ? new UnSuccessfulResult(BusinessMessages.UserNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

    }
}
