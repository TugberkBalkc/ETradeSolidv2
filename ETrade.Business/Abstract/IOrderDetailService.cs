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
    public interface IOrderDetailService
    {
        IResult Add(OrderDetail orderDetail);
        IResult Update(OrderDetail orderDetail);
        IResult Delete(int orderDetailId);
        IResult HardDelete(OrderDetail orderDetail);

        IDataResult<ObjectDto<OrderDetail>> GetById(int orderDetailId);

        IDataResult<ObjectQueryableDto<OrderDetail>> GetAll();
        IDataResult<ObjectQueryableDto<OrderDetail>> GetAllByDiscount(double minDiscount);
        IDataResult<ObjectQueryableDto<OrderDetail>> GetAllByDiscount(double minDiscount, double maxDiscount);

        IDataResult<ObjectQueryableDto<OrderDetail>> GetAllDiscount();
        IDataResult<ObjectQueryableDto<OrderDetail>> GetAllActive();
        IDataResult<ObjectQueryableDto<OrderDetail>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<OrderDetail>> GetAllActiveAndNonDeleted();
    }
}
