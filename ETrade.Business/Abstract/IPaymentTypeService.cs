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
    public interface IPaymentTypeService
    {
        IResult Add(PaymentType paymentType);
        IResult Update(PaymentType paymentType);
        IResult Delete(int paymentTypeId);
        IResult HardDelete(PaymentType paymentType);

        IDataResult<ObjectDto<PaymentType>> GetById(int paymentTypeId);

        IDataResult<ObjectQueryableDto<PaymentType>> GetAll();

        IDataResult<ObjectQueryableDto<PaymentType>> GetAllActive();
        IDataResult<ObjectQueryableDto<PaymentType>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<PaymentType>> GetAllActiveAndNonDeleted();
    }
}
