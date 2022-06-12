using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.Role;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleCommandRepository _roleCommandRepository;
        private readonly IRoleQueryRepository _roleQueryRepository;

        public RoleManager(IRoleQueryRepository roleQueryRepository,
            IRoleCommandRepository roleCommandRepository)
        {
            _roleCommandRepository = roleCommandRepository;
            _roleQueryRepository = roleQueryRepository;
        }

        public IResult Add(Role role)
        {
            var logicResult =
              BusinessLogicEngine.Run
              (CheckIfRoleAddedBefore(role.Name));

            if (logicResult != null)
            {
                return logicResult;
            }

            var result = 
                _roleCommandRepository.Add(role);
            _roleCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.RoleAdded, BusinessMessages.RoleCouldNotAdded);
        }

        public IResult Delete(int roleId)
        {
            var logicResult =
             BusinessLogicEngine.Run
             (CheckIfRoleExists(roleId));

            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _roleQueryRepository.Get(r => r.Id == roleId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _roleCommandRepository.Update(entity);
            _roleCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.RoleDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<Role>> GetAll()
        {
            var result = _roleQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.RolesFoundDueToFilter, BusinessMessages.AnyRolesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Role>> GetAllActive()
        {
            var result = _roleQueryRepository.GetAll(r=>r.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.RolesFoundDueToFilter, BusinessMessages.AnyRolesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Role>> GetAllActiveAndNonDeleted()
        {
            var result = _roleQueryRepository.GetAll(r=>r.IsActive && !r.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.RolesFoundDueToFilter, BusinessMessages.AnyRolesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Role>> GetAllNonDeleted()
        {
            var result = _roleQueryRepository.GetAll(r=>!r.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.RolesFoundDueToFilter, BusinessMessages.AnyRolesFoundDueToFilter);
        }

        public IDataResult<ObjectDto<Role>> GetById(int roleId)
        {
            var result = _roleQueryRepository.Get(r => r.Id == roleId);
            return CheckObjectReturnValue(result, BusinessMessages.RoleFoundDueToFilter, BusinessMessages.AnyRoleFoundDueToFilter);
        }

        public IResult HardDelete(Role role)
        {
            var logicResult =
             BusinessLogicEngine.Run
             (CheckIfRoleExists(role.Id));
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _roleQueryRepository.Get(r => r.Id == role.Id);

            _roleCommandRepository.HardDelete(entity);
            _roleCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.RoleHardDeleted, BusinessTitles.Successful);
        }

        public IResult Update(Role role)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfRoleExists(role.Id));

            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _roleCommandRepository.Update(role);
            _roleCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.RoleUpdated, BusinessMessages.RoleCouldNotUpdated);
        }
        public IDataResult<ObjectQueryableDto<OperationClaim>> GetOperationClaimsByRoleId(int roleId)
        {
            var logicResult =
                 BusinessLogicEngine.Run
                 (CheckIfRoleExistsInOperationClaims(roleId));
            if (logicResult != null)
            {
                return (IDataResult<ObjectQueryableDto<OperationClaim>>)logicResult;
            }

            var userResult = this.GetById(roleId);
            var result = _roleQueryRepository.GetOperationClaimsByRoleId(roleId);

            if (result.Count() > -1)
            {
                ObjectQueryableDto<OperationClaim> objectQueryableDto = new ObjectQueryableDto<OperationClaim>
                {
                    Entities = result,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<OperationClaim>>(objectQueryableDto, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<OperationClaim>>(BusinessMessages.AnyOperationClaimsFoundDueToFilter, BusinessTitles.Warning);
        }




        //Business Utilities

        private IDataResult<ObjectDto<Role>> CheckObjectReturnValue(Role role, string successMessage, string unSuccessMessage)
        {

            if (role != null)
            {
                ObjectDto<Role> objectDto = new ObjectDto<Role>
                {
                    Entity = role,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<Role>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<Role>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<Role>> CheckObjectsReturnValue(IQueryable<Role> roles, string successMessage, string unSuccessMessage)
        {
            if (roles.Count() > -1)
            {
                ObjectQueryableDto<Role> objectQueryableDto = new ObjectQueryableDto<Role>
                {
                    Entities = roles,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<Role>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<Role>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules

        private IResult CheckIfRoleAddedBefore(string name)
        {
            bool status = false;
            var roles = this.GetAll();
            foreach (var item in roles.Data.Entities)
            {
                if (item.Name.Trim().ToLower() == name.Trim().ToLower())
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.RoleExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> CheckIfRoleExistsInOperationClaims(int roleId)
        {
            var roles = _roleQueryRepository.GetAll();
            foreach (var item in roles)
            {
                if (item.Id == roleId)
                {
                    return new SuccessfulDataResult<ObjectQueryableDto<OperationClaim>>();
                }
            }
            var queryable = new List<OperationClaim>().AsQueryable<OperationClaim>();
            return new UnSuccessfulDataResult<ObjectQueryableDto<OperationClaim>>(new ObjectQueryableDto<OperationClaim> { Entities = queryable, ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.UnSuccessful }, BusinessMessages.UserNotFound, BusinessTitles.Warning);
        }

        public IResult CheckIfRoleExists(int roleId)
        {
            var roles = _roleQueryRepository.GetAll();
            foreach (var item in roles)
            {
                if (item.Id == roleId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.RoleNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfUserNull(Role role)
        {
            return role == null
                ? new UnSuccessfulResult(BusinessMessages.RoleNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
    }
}
