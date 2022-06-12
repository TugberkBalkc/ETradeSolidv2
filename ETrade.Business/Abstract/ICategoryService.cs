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
    public interface ICategoryService
    {
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(int categoryId);
        IResult HardDelete(Category category);

        IDataResult<ObjectDto<Category>> GetById(int categoryId);

        IDataResult<ObjectQueryableDto<Category>> GetAll();

        IDataResult<ObjectQueryableDto<Category>> GetAllActive();
        IDataResult<ObjectQueryableDto<Category>> GetAllNonDeleted();
        IDataResult<ObjectQueryableDto<Category>> GetAllActiveAndNonDeleted();
    }
}
