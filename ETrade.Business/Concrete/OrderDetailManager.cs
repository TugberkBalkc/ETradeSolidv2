using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.OrderDetail;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailQueryRepository _orderDetailQueryRepository;
        private readonly IOrderDetailCommandRepository _orderDetailCommandRepository;

        public OrderDetailManager(IOrderDetailQueryRepository orderDetailQueryRepository, IOrderDetailCommandRepository orderDetailCommandRepository)
        {
            _orderDetailQueryRepository = orderDetailQueryRepository;
            _orderDetailCommandRepository = orderDetailCommandRepository;
        }
        //public int OrderId { get; set; }
        //public int ProductId { get; set; }
        //public decimal UnitPrice { get; set; }
        //public short Quantity { get; set; }
        //public double Discount { get; set; } = 0;
        public IResult Add(OrderDetail orderDetail)
        {
            var result = _orderDetailCommandRepository.Add(orderDetail);
            _orderDetailCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.OrderDetailAdded, BusinessMessages.OrderDetailCouldNotAdded);
        }

        public IResult Delete(int orderDetailId)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfOrderDetailExists(orderDetailId));

            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _orderDetailQueryRepository.Get(od => od.Id == orderDetailId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _orderDetailCommandRepository.Update(entity);
            _orderDetailCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.OrderDetailAdded, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<OrderDetail>> GetAll()
        {
            var result = _orderDetailQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OrderDetail>> GetAllActive()
        {
            var result = _orderDetailQueryRepository.GetAll(od => od.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OrderDetail>> GetAllActiveAndNonDeleted()
        {
            var result = _orderDetailQueryRepository.GetAll(od => od.IsActive && !od.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OrderDetail>> GetAllByDiscount(double minDiscount)
        {
            var result = _orderDetailQueryRepository.GetAll(od => od.Discount >= minDiscount);
            return CheckObjectsReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OrderDetail>> GetAllByDiscount(double minDiscount, double maxDiscount)
        {
            var result = _orderDetailQueryRepository.GetAll(od => od.Discount >= minDiscount && od.Discount <= maxDiscount);
            return CheckObjectsReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OrderDetail>> GetAllDiscount()
        {
            var result = _orderDetailQueryRepository.GetAll(od => od.Discount != 0);
            return CheckObjectsReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<OrderDetail>> GetAllNonDeleted()
        {
            var result = _orderDetailQueryRepository.GetAll(od => !od.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IDataResult<ObjectDto<OrderDetail>> GetById(int orderDetailId)
        {
            var result = _orderDetailQueryRepository.Get(od => od.Id == orderDetailId);
            return CheckObjectReturnValue(result, BusinessMessages.OrderDetailFoundDueToFilter, BusinessMessages.AnyOrderDetailsFoundDueToFilter);
        }

        public IResult HardDelete(OrderDetail orderDetail)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfOrderDetailExists(orderDetail.Id));
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _orderDetailQueryRepository.Get(a => a.Id == orderDetail.Id);

            _orderDetailCommandRepository.HardDelete(entity);
            _orderDetailCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.OrderDetailDeleted, BusinessTitles.Successful);
        }

        public IResult Update(OrderDetail orderDetail)
        {
            var logicResult =
             BusinessLogicEngine.Run
             (CheckIfOrderDetailExists(orderDetail.Id));

            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _orderDetailCommandRepository.Update(orderDetail);
            _orderDetailCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.AddressUpdated, BusinessMessages.AddressCouldNotUpdated);
        }





        //Business Utilities

        private IDataResult<ObjectDto<OrderDetail>> CheckObjectReturnValue(OrderDetail orderDetail, string successMessage, string unSuccessMessage)
        {

            if (orderDetail != null)
            {
                ObjectDto<OrderDetail> objectDto = new ObjectDto<OrderDetail>
                {
                    Entity = orderDetail,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<OrderDetail>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<OrderDetail>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<OrderDetail>> CheckObjectsReturnValue(IQueryable<OrderDetail> orderDetails, string successMessage, string unSuccessMessage)
        {
            if (orderDetails.Count() > -1)
            {
                ObjectQueryableDto<OrderDetail> objectQueryableDto = new ObjectQueryableDto<OrderDetail>
                {
                    Entities = orderDetails,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<OrderDetail>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<OrderDetail>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules
        private IResult CheckIfOrderDetailExists(int orderDetailId)
        {
            var orderDetails = _orderDetailQueryRepository.GetAll();
            foreach (var item in orderDetails)
            {
                if (item.Id == orderDetailId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.OrderDetailNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfOrderDetailNull(OrderDetail orderDetail)
        {
            return orderDetail == null
                ? new UnSuccessfulResult(BusinessMessages.OrderDetailNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
    }
}
