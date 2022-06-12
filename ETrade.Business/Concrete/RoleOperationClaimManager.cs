using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Abstract;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.UserOperationClaim;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class RoleOperationClaimManager : IRoleOperationClaimService
    {
        private readonly IRoleOperationClaimQueryRepository _roleOperationClaimQueryRepository;
        private readonly IRoleOperationClaimCommandRepository _roleOperationClaimCommandRepository;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IRoleService _roleService;
        public RoleOperationClaimManager(IRoleOperationClaimQueryRepository roleOperationClaimQueryRepository,
            IRoleOperationClaimCommandRepository roleOperationClaimCommandRepository,
            IOperationClaimService operationClaimService,
            IRoleService roleService)
        {
            _roleOperationClaimQueryRepository = roleOperationClaimQueryRepository;
            _roleOperationClaimCommandRepository = roleOperationClaimCommandRepository;
            _operationClaimService = operationClaimService;
            _roleService = roleService;
        }

        public IResult AddClaimToRole(int roleId, int operationClaimId)
        {
            var logicResult =
            BusinessLogicEngine.Run
            (_roleService.CheckIfRoleExists(roleId),
             _operationClaimService.CheckIfOperationClaimExists(operationClaimId),
             CheckIfRoleOperationClaimAddedBefore(roleId, operationClaimId));             

            if (logicResult != null)
            {
                return logicResult;
            }

            var roleOperationClaim = new RoleOperationClaim
            {
                RoleId = roleId,
                OperationClaimId = operationClaimId
            };

            var result = _roleOperationClaimCommandRepository.Add(roleOperationClaim);
            _roleOperationClaimCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.RoleOperationClaimAdded, BusinessMessages.RoleOperationClaimCouldNotAdded);
        }


        public IResult Delete(int roleId, int operationClaimId)
        {
            var logicResult =
             BusinessLogicEngine.Run
             (CheckIfRoleOperationClaimExists(roleId, operationClaimId));

            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _roleOperationClaimQueryRepository.Get(roc => roc.Id == roleId && roc.OperationClaimId == operationClaimId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _roleOperationClaimCommandRepository.Update(entity);
            _roleOperationClaimCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.RoleOperationClaimDeleted, BusinessTitles.Successful);
        }

        public IResult HardDelete(RoleOperationClaim roleOperationClaim)
        {
            var logicResult =
          BusinessLogicEngine.Run
          (CheckIfRoleOperationClaimExists(roleOperationClaim.RoleId, roleOperationClaim.OperationClaimId));
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _roleOperationClaimQueryRepository.Get(roc => roc.Id == roleOperationClaim.Id);

            _roleOperationClaimCommandRepository.HardDelete(entity);
            _roleOperationClaimCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.RoleOperationClaimHardDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetOperationClaimsByRoleId(int roleId)
        {
            var result = _roleService.GetOperationClaimsByRoleId(roleId);
            return CheckObjectsReturnValue<OperationClaim>(result.Data.Entities, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveOperationClaimsByRoleId(int roleId)
        {
            var result = _roleService.GetOperationClaimsByRoleId(roleId);
            var query = result.Data.Entities.AsQueryable().Where(opc => opc.IsActive);
            return CheckObjectsReturnValue<OperationClaim>(query, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetNonDeletedOperationClaimsByRoleId(int roleId)
        {
            var result = _roleService.GetOperationClaimsByRoleId(roleId);
            var query = result.Data.Entities.AsQueryable().Where(opc => !opc.IsDeleted);
            return CheckObjectsReturnValue<OperationClaim>(query, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetActiveAndNonDeletedOperationClaimsByRoleId(int roleId)
        {
            var result = _roleService.GetOperationClaimsByRoleId(roleId);
            var query = result.Data.Entities.AsQueryable().Where(opc => opc.IsActive && !opc.IsDeleted);
            return CheckObjectsReturnValue<OperationClaim>(query, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }


        //Business Utilities

        private IDataResult<ObjectDto<RoleOperationClaim>> CheckObjectReturnValue(RoleOperationClaim roleOperationClaim, string successMessage, string unSuccessMessage)
        {

            if (roleOperationClaim != null)
            {
                ObjectDto<RoleOperationClaim> objectDto = new ObjectDto<RoleOperationClaim>
                {
                    Entity = roleOperationClaim,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<RoleOperationClaim>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<RoleOperationClaim>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<RoleOperationClaim>> CheckObjectsReturnValue(IQueryable<RoleOperationClaim> roleOperationClaims, string successMessage, string unSuccessMessage)
        {
            if (roleOperationClaims.Count() > -1)
            {
                ObjectQueryableDto<RoleOperationClaim> objectQueryableDto = new ObjectQueryableDto<RoleOperationClaim>
                {
                    Entities = roleOperationClaims,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<RoleOperationClaim>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<RoleOperationClaim>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<T>> CheckObjectsReturnValue<T>(IQueryable<T> TEntities, string successMessage, string unSuccessMessage)
        where T : class, IEntity, new()
        {
            if (TEntities.Count() > -1)
            {
                ObjectQueryableDto<T> objectQueryableDto = new ObjectQueryableDto<T>
                {
                    Entities = TEntities,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<T>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<T>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules

        private IResult CheckIfRoleOperationClaimAddedBefore(int roleId, int operationClaimId)
        {
            bool status = false;
            var roleOperationClaims = _roleOperationClaimQueryRepository.GetAll();
            foreach (var item in roleOperationClaims)
            {
                if (item.RoleId == roleId && item.OperationClaimId == operationClaimId)
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.RoleOperationClaimExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        private IResult CheckIfUserOperationClaimExists(int roleOperationClaimId)
        {
            var roleOperationClaims = _roleOperationClaimQueryRepository.GetAll();
            foreach (var item in roleOperationClaims)
            {
                if (item.Id == roleOperationClaimId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.RoleOperationClaimNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfRoleOperationClaimExists(int roleId, int operationClaimId)
        {
            var roleOperationClaims = _roleOperationClaimQueryRepository.GetAll();
            foreach (var item in roleOperationClaims)
            {
                if (item.RoleId == roleId && item.OperationClaimId == operationClaimId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.RoleOperationClaimNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfRoleOperationClaimNull(RoleOperationClaim userOperationClaim)
        {
            return userOperationClaim == null
                ? new UnSuccessfulResult(BusinessMessages.RoleOperationClaimNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

    }
}
