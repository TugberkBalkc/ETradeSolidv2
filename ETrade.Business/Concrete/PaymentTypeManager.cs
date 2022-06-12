using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.PaymentType;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class PaymentTypeManager : IPaymentTypeService
    {
        private readonly IPaymentTypeQueryRepository _paymentTypeQueryRepository;
        private readonly IPaymentTypeCommandRepository _paymentTypeCommandRepository;

        public PaymentTypeManager(IPaymentTypeQueryRepository paymentTypeQueryRepository,
                              IPaymentTypeCommandRepository paymentTypeCommandRepository)
        {
            _paymentTypeQueryRepository = paymentTypeQueryRepository;
            _paymentTypeCommandRepository = paymentTypeCommandRepository;
        }


        public IResult Add(PaymentType paymentType)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfPaymentTypeAddedBefore(paymentType.Name));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _paymentTypeCommandRepository.Add(paymentType);
            _paymentTypeCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.PaymentTypeAdded, BusinessMessages.PaymentTypeCouldNotAdded);
        }

        public IResult Delete(int paymentTypeId)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfPaymentTypeExists(paymentTypeId));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _paymentTypeQueryRepository.Get(pt => pt.Id == paymentTypeId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _paymentTypeCommandRepository.Update(entity);
            _paymentTypeCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.PaymentTypeDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<PaymentType>> GetAll()
        {
            var result = _paymentTypeQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.PaymentTypesFoundDueToFilter, BusinessMessages.AnyPaymentTypesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<PaymentType>> GetAllActive()
        {
            var result = _paymentTypeQueryRepository.GetAll(pt=>pt.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.PaymentTypesFoundDueToFilter, BusinessMessages.AnyPaymentTypesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<PaymentType>> GetAllActiveAndNonDeleted()
        {
            var result = _paymentTypeQueryRepository.GetAll(pt => pt.IsActive && !pt.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.PaymentTypesFoundDueToFilter, BusinessMessages.AnyPaymentTypesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<PaymentType>> GetAllNonDeleted()
        {
            var result = _paymentTypeQueryRepository.GetAll(pt => !pt.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.PaymentTypesFoundDueToFilter, BusinessMessages.AnyPaymentTypesFoundDueToFilter);
        }

        public IDataResult<ObjectDto<PaymentType>> GetById(int paymentTypeId)
        {
            var result = _paymentTypeQueryRepository.Get(pt => pt.Id == paymentTypeId);
            return CheckObjectReturnValue(result, BusinessMessages.PaymentTypeFoundDueToFilter, BusinessMessages.AnyPaymentTypeFoundDueToFilter);
        }

        public IResult HardDelete(PaymentType paymentType)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfPaymentTypeExists(paymentType.Id));
          
            if (logicResult != null)
            {
                return logicResult;
            }

            _paymentTypeCommandRepository.HardDelete(paymentType);
            _paymentTypeCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.PaymentTypeHardDeleted, BusinessTitles.Successful);
        }

        public IResult Update(PaymentType paymentType)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfPaymentTypeExists(paymentType.Id));
   
            if (logicResult != null)
            {
                return logicResult;
            }


            var result = _paymentTypeCommandRepository.Update(paymentType);
            _paymentTypeCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.PaymentTypeUpdated, BusinessMessages.PaymentTypeCouldNotUpdated);
        }




        //Business Utilities

        private IDataResult<ObjectDto<PaymentType>> CheckObjectReturnValue(PaymentType paymentType, string successMessage, string unSuccessMessage)
        {

            if (paymentType != null)
            {
                ObjectDto<PaymentType> objectDto = new ObjectDto<PaymentType>
                {
                    Entity = paymentType,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<PaymentType>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<PaymentType>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<PaymentType>> CheckObjectsReturnValue(IQueryable<PaymentType> paymentTypes, string successMessage, string unSuccessMessage)
        {
            if (paymentTypes.Count() > -1)
            {
                ObjectQueryableDto<PaymentType> objectQueryableDto = new ObjectQueryableDto<PaymentType>
                {
                    Entities = paymentTypes,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<PaymentType>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<PaymentType>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules

        private IResult CheckIfPaymentTypeAddedBefore(string name)
        {
            bool status = false;
            var paymentTypes = this.GetAll();
            foreach (var item in paymentTypes.Data.Entities)
            {
                if (item.Name.Trim().ToLower() == name.Trim().ToLower())
                {
                    status = true;
                }
            }


            return status == true
                ? new UnSuccessfulResult(BusinessMessages.PaymentTypeExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        private IResult CheckIfPaymentTypeExists(int paymentTypeId)
        {
            var paymentTypes = _paymentTypeQueryRepository.GetAll();
            foreach (var item in paymentTypes)
            {
                if (item.Id == paymentTypeId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.PaymentTypeNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfPaymenTypeNull(PaymentType paymentType)
        {
            return paymentType == null
                ? new UnSuccessfulResult(BusinessMessages.PaymentTypeNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
    }
}
