using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.Order;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly IOrderCommandRepository _orderCommandRepository;

        public OrderManager(IOrderQueryRepository orderQueryRepository, IOrderCommandRepository orderCommandRepository)
        {
            _orderQueryRepository = orderQueryRepository;
            _orderCommandRepository = orderCommandRepository;
        }

        public IResult Add(Order order)
        {
            var result = _orderCommandRepository.Add(order);
            _orderCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.OrderAdded, BusinessMessages.OrderCouldNotAdded);
        }

        public IResult Delete(int orderId)
        {
            var logicResult = BusinessLogicEngine.Run(CheckIfOrderExists(orderId));
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _orderQueryRepository.Get(o => o.Id == orderId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _orderCommandRepository.Update(entity);
            _orderCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.OrderDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAll()
        {
            var result = _orderQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllActive()
        {
            var result = _orderQueryRepository.GetAll(o => o.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllActiveAndNonDeleted()
        {
            var result = _orderQueryRepository.GetAll(o => o.IsActive && !o.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllByAddressI(int addressId)
        {
            var result = _orderQueryRepository.GetAll(o=>o.AddressId == addressId);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllByPaymentId(int paymentId)
        {
            var result = _orderQueryRepository.GetAll(o => o.PaymentId == paymentId);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllByUserId(int userId)
        {
            var result = _orderQueryRepository.GetAll(o => o.UserId == userId);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllDelivered()
        {
            var result = _orderQueryRepository.GetAll(o=>o.IsDelivered);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllNonDeleted()
        {
            var result = _orderQueryRepository.GetAll(o => !o.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Order>> GetAllNonDelivered()
        {
            var result = _orderQueryRepository.GetAll(o => !o.IsDelivered);
            return CheckObjectsReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IDataResult<ObjectDto<Order>> GetById(int orderId)
        {
            var result = _orderQueryRepository.Get(o => o.Id == orderId);
            return CheckObjectReturnValue(result, BusinessMessages.OrdersFoundDueToFilter, BusinessMessages.AnyOrdersFoundDueToFilter);
        }

        public IResult HardDelete(Order order)
        {
            var logicResult = BusinessLogicEngine.Run(CheckIfOrderExists(order.Id));
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _orderQueryRepository.Get(a => a.Id == order.Id);

            _orderCommandRepository.HardDelete(entity);
            _orderCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.OrderDeleted, BusinessTitles.Successful);
        }

        public IResult Update(Order order)
        {
            var logicResult =
            BusinessLogicEngine.Run
            (CheckIfOrderExists(order.Id));

            if (logicResult != null)
            {
                return logicResult;
            }


            var result = _orderCommandRepository.Update(order);
            _orderCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.OrderUpdated, BusinessMessages.OrderCouldNotUpdated);
        }




        //Business Utilities

        private IDataResult<ObjectDto<Order>> CheckObjectReturnValue(Order order, string successMessage, string unSuccessMessage)
        {

            if (order != null)
            {
                ObjectDto<Order> objectDto = new ObjectDto<Order>
                {
                    Entity = order,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<Order>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<Order>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<Order>> CheckObjectsReturnValue(IQueryable<Order> orders, string successMessage, string unSuccessMessage)
        {
            if (orders.Count() > -1)
            {
                ObjectQueryableDto<Order> objectQueryableDto = new ObjectQueryableDto<Order>
                {
                    Entities = orders,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<Order>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<Order>>(unSuccessMessage, BusinessTitles.Warning);
        }




        //Business Rules
        private IResult CheckIfOrderExists(int orderId)
        {
            var orders = _orderQueryRepository.GetAll();
            foreach (var item in orders)
            {
                if (item.Id == orderId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.OrderNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfOrderNull(Order order)
        {
            return order == null
                ? new UnSuccessfulResult(BusinessMessages.OrderNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
    }
}
