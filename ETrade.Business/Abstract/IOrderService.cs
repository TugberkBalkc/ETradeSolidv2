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
    public interface IOrderService
    {
        IResult Add(Order order);
        IResult Update(Order order);
        IResult Delete(int orderId);
        IResult HardDelete(Order order);

        IDataResult<ObjectDto<Order>> GetById(int orderId);

        IDataResult<ObjectQueryableDto<Order>> GetAll();
        IDataResult<ObjectQueryableDto<Order>> GetAllByUserId(int userId);
        IDataResult<ObjectQueryableDto<Order>> GetAllByPaymentId(int paymentId);
        IDataResult<ObjectQueryableDto<Order>> GetAllByAddressI(int addressId);

        IDataResult<ObjectQueryableDto<Order>> GetAllDelivered();
        IDataResult<ObjectQueryableDto<Order>> GetAllNonDelivered();
        IDataResult<ObjectQueryableDto<Order>> GetAllActive();
        IDataResult<ObjectQueryableDto<Order>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<Order>> GetAllActiveAndNonDeleted();
    }
}
