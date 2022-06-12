using ETrade.Business.Abstract;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.Category;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly ICategoryCommandRepository _categoryCommandRepository;

        public CategoryManager(ICategoryQueryRepository categoryQueryRepository, ICategoryCommandRepository categoryCommandRepository)
        {
            _categoryQueryRepository = categoryQueryRepository;
            _categoryCommandRepository = categoryCommandRepository;
        }

        public IResult Add(Category category)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfCategoryAddedBefore(category.Name));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _categoryCommandRepository.Add(category);
            _categoryCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.CategoryAdded, BusinessMessages.CategoryCouldNotAdded);
        }

        public IResult Delete(int categoryId)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfCategoryExists(categoryId));

            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _categoryQueryRepository.Get(c => c.Id == categoryId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _categoryCommandRepository.Update(entity);
            _categoryCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.CategoryDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<Category>> GetAll()
        {
            var result = _categoryQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.CategoriesFoundDueToFilter, BusinessMessages.AnyCategoriesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Category>> GetAllActive()
        {
            var result = _categoryQueryRepository.GetAll(c => c.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.CategoriesFoundDueToFilter, BusinessMessages.AnyCategoriesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Category>> GetAllActiveAndNonDeleted()
        {
            var result = _categoryQueryRepository.GetAll(c => c.IsActive && !c.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.CategoriesFoundDueToFilter, BusinessMessages.AnyCategoriesFoundDueToFilter);
        }

        public IDataResult<ObjectQueryableDto<Category>> GetAllNonDeleted()
        {
            var result = _categoryQueryRepository.GetAll(c => !c.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.CategoriesFoundDueToFilter, BusinessMessages.AnyCategoriesFoundDueToFilter);
        }

        public IDataResult<ObjectDto<Category>> GetById(int categoryId)
        {
            var result = _categoryQueryRepository.Get(c => c.Id == categoryId);
            return CheckObjectReturnValue(result, BusinessMessages.CategoryFoundDueToFilter, BusinessMessages.AnyCategoriesFoundDueToFilter);
        }

        public IResult HardDelete(Category category)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfCategoryExists(category.Id));
            
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _categoryQueryRepository.Get(a => a.Id == category.Id);

            _categoryCommandRepository.HardDelete(entity);
            _categoryCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.CategoryHardDeleted, BusinessTitles.Successful);
        }

        public IResult Update(Category category)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfCategoryExists(category.Id));
            
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _categoryCommandRepository.Update(category);
            _categoryCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.AddressUpdated, BusinessMessages.AddressCouldNotUpdated);
        }




        //Business Utilities

        private IDataResult<ObjectDto<Category>> CheckObjectReturnValue(Category category, string successMessage, string unSuccessMessage)
        {

            if (category != null)
            {
                ObjectDto<Category> objectDto = new ObjectDto<Category>
                {
                    Entity = category,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<Category>>(objectDto, successMessage, "Successful");
            }
            else  return new UnSuccessfulDataResult<ObjectDto<Category>>(unSuccessMessage, "Warning");
        }

        private IDataResult<ObjectQueryableDto<Category>> CheckObjectsReturnValue(IQueryable<Category> categories, string successMessage, string unSuccessMessage)
        {
            if (categories.Count() > -1)
            {
                ObjectQueryableDto<Category> objectQueryableDto = new ObjectQueryableDto<Category>
                {
                    Entities = categories,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<Category>>(objectQueryableDto, successMessage, "Successful");
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<Category>>(unSuccessMessage, "Warning");
        }




        //Business Rules

        private IResult CheckIfCategoryAddedBefore(string name)
        {
            bool status = false;
            var categories = this.GetAll();
            foreach (var item in categories.Data.Entities)
            {
                if (item.Name.Trim().ToLower() == name.Trim().ToLower())
                {
                    status = true;
                }
            }

            return status == true
                ? new UnSuccessfulResult(BusinessMessages.AddressExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
        private IResult CheckIfCategoryExists(int categoryId)
        {
            var categories = _categoryQueryRepository.GetAll();
            foreach (var item in categories)
            {
                if (item.Id == categoryId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.CategoryNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfCategoryNull(Category category)
        {
            return category == null
                ? new UnSuccessfulResult(BusinessMessages.AddressNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }
    }
}
