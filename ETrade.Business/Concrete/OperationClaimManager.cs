using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Entities.Concrete;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.OperationClaim;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimQueryRepository _operationClaimQueryRepository;
        private readonly IOperationClaimCommandRepository _operationClaimCommandRepository;

        public OperationClaimManager(IOperationClaimQueryRepository operationClaimQueryRepository,
            IOperationClaimCommandRepository operationClaimCommandRepository)
        {
            _operationClaimQueryRepository = operationClaimQueryRepository;
            _operationClaimCommandRepository = operationClaimCommandRepository;
        }

        public IResult Add(OperationClaim operationClaim)
        {
            var logicResult =
             BusinessLogicEngine.Run
             (CheckIfOperationClaimAddedBefore(operationClaim.Name));

            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _operationClaimCommandRepository.Add(operationClaim);
            _operationClaimCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.OperationClaimAdded, BusinessMessages.OperationClaimCouldNotAdded);
        }

        public IResult Delete(int operationClaimId)
        {
            var logicResult =
                 BusinessLogicEngine.Run
                 (CheckIfOperationClaimExists(operationClaimId));

            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _operationClaimQueryRepository.Get(opc => opc.Id == operationClaimId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _operationClaimCommandRepository.Update(entity);
            _operationClaimCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.OperationClaimDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetAll()
        {
            var result = _operationClaimQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetAllActive()
        {
            var result = _operationClaimQueryRepository.GetAll(opc=>opc.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetAllActiveAndNonDeleted()
        {
            var result = _operationClaimQueryRepository.GetAll(opc => opc.IsActive && !opc.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OperationClaim>> GetAllNonDeleted()
        {
            var result = _operationClaimQueryRepository.GetAll(opc=>!opc.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.OperationClaimsFoundDueToFilter, BusinessMessages.AnyOperationClaimsFoundDueToFilter);
        }

        public IDataResult<ObjectDto<OperationClaim>> GetById(int operationClaimId)
        {
            var result = _operationClaimQueryRepository.Get(opc => opc.Id == operationClaimId);
            return CheckObjectReturnValue(result, BusinessMessages.OperationClaimFoundDueToFilter, BusinessMessages.AnyOperationClaimFoundDueToFilter);
        }

        public IResult HardDelete(OperationClaim operationClaim)
        {
            var logicResult =
             BusinessLogicEngine.Run
             (CheckIfOperationClaimExists(operationClaim.Id));

            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _operationClaimQueryRepository.Get(opc => opc.Id == operationClaim.Id);

            _operationClaimCommandRepository.HardDelete(entity);
            _operationClaimCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.OperationClaimHardDeleted, BusinessTitles.Successful);
        }

        public IResult Update(OperationClaim operationClaim)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfOperationClaimExists(operationClaim.Id));

            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _operationClaimCommandRepository.Update(operationClaim);
            _operationClaimCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.OperationClaimUpdated, BusinessMessages.OperationClaimCouldNotUpdated);
        }




        //Business Utilities

        private IDataResult<ObjectDto<OperationClaim>> CheckObjectReturnValue(OperationClaim operationClaim, string successMessage, string unSuccessMessage)
        {

            if (operationClaim != null)
            {
                ObjectDto<OperationClaim> objectDto = new ObjectDto<OperationClaim>
                {
                    Entity = operationClaim,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<OperationClaim>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<OperationClaim>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<OperationClaim>> CheckObjectsReturnValue(IQueryable<OperationClaim> operationClaims, string successMessage, string unSuccessMessage)
        {
            if (operationClaims.Count() > -1)
            {
                ObjectQueryableDto<OperationClaim> objectQueryableDto = new ObjectQueryableDto<OperationClaim>
                {
                    Entities = operationClaims,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<OperationClaim>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<OperationClaim>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules

        private IResult CheckIfOperationClaimAddedBefore(string name)
        {
            bool status = false;
            var operationClaims = this.GetAll();
            foreach (var item in operationClaims.Data.Entities)
            {
                if (item.Name.Trim().ToLower() == name.Trim().ToLower())
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.OperationClaimExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        public IResult CheckIfOperationClaimExists(int operationClaimId)
        {
            var operationClaims = _operationClaimQueryRepository.GetAll();
            foreach (var item in operationClaims)
            {
                if (item.Id == operationClaimId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.OperationClaimNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfOperationClaimNull(OperationClaim operationClaim)
        {
            return operationClaim == null
                ? new UnSuccessfulResult(BusinessMessages.OperationClaimNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

    }
}
