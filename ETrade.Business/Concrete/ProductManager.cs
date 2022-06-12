using ETrade.Business.Abstract;
using ETrade.Business.BusinessAspects.CastleDynamicProxy;
using ETrade.Business.Constants.BusinessMessages;
using ETrade.Business.Constants.BusinessTitles;
using ETrade.Business.ValidationRules.FluentValidation;
using ETrade.Core.Aspects.Exception;
using ETrade.Core.Aspects.Logging;
using ETrade.Core.Aspects.Validation;
using ETrade.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ETrade.Core.Utilities.Business.LogicEngine;
using ETrade.Core.Utilities.Results.DataResult;
using ETrade.Core.Utilities.Results.Result;
using ETrade.DataAccess.Abstract.Product;
using ETrade.Entities.Concrete;
using ETrade.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{

    public class ProductManager : IProductService
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductCommandRepository _productCommandRepository;

        public ProductManager(IProductQueryRepository productQueryRepository,
                              IProductCommandRepository productCommandRepository)
        {
            _productQueryRepository = productQueryRepository;
            _productCommandRepository = productCommandRepository;
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            var logicResult =
                BusinessLogicEngine.Run
                (CheckIfProductAddedBefore(product.CategoryId, product.Name));
            
            if (logicResult != null)
            {
                return logicResult;
            }

            var result = _productCommandRepository.Add(product);
            _productCommandRepository.SaveChanges();
            return CheckObjectReturnValue(result, BusinessMessages.ProductAdded, BusinessMessages.ProductCouldNotAdded);
        }

        [SecuredOperationAspect("product.delete")]
        public IResult Delete(int productId)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfProductExists(productId));
            
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _productQueryRepository.Get(p => p.Id == productId);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            var result = _productCommandRepository.Update(entity);
            _productCommandRepository.SaveChanges();

            return new SuccessfulResult(BusinessMessages.AddressDeleted, BusinessTitles.Successful);
        }

        public IDataResult<ObjectQueryableDto<ProductDetailsDto>> GetProductDetailsByCategoryId(int categoryId = 0)
        {
            IQueryable<ProductDetailsDto> result;
            if (categoryId == 0)
            {
                result = _productQueryRepository.GetAllProductDetails();
            }
            else
            {
                result = _productQueryRepository.GetAllProductDetails(p => p.CategoryId == categoryId);
            }
            return CheckObjectsReturnValue<ProductDetailsDto>(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        //[SecuredOperationAspect("product.list.details")]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<ObjectQueryableDto<ProductDetailsDto>> GetProductDetails()
        {
            var result = _productQueryRepository.GetAllProductDetails();
            return CheckObjectsReturnValue<ProductDetailsDto>(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.all")]
        public IDataResult<ObjectQueryableDto<Product>> GetAll()
        {
            var result = _productQueryRepository.GetAll();
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.name")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllByName(string productName)
        {
            var result = _productQueryRepository.GetAll(p=>p.Name.Trim().ToLower().Contains(productName.Trim().ToLower()));
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.active")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllActive()
        {
            var result = _productQueryRepository.GetAll(p=>p.IsActive);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.active, product.list.nondeleted")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllActiveAndNonDeleted()
        {
            var result = _productQueryRepository.GetAll(p=>p.IsActive && !p.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.categoryid")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllByCategoryId(int categoryId)
        {
            var result = _productQueryRepository.GetAll(p=>p.CategoryId == categoryId);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.stockamount")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllByStockAmount(short minStockAmount)
        {
            var result = _productQueryRepository.GetAll(p=>p.StockAmount >= minStockAmount);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.stockamount")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllByStockAmount(short minStockAmount, short maxStockAmount)
        {
            var result = _productQueryRepository.GetAll(p => p.StockAmount >= minStockAmount && p.StockAmount <=maxStockAmount);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.unitprice")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllByUnitPrice(decimal minUnitPrice)
        {
            var result = _productQueryRepository.GetAll(p=>p.UnitPrice >= minUnitPrice);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.list.unitprice")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllByUnitPrice(decimal minUnitPrice, decimal maxUnitPrice)
        {
            var result = _productQueryRepository.GetAll(p => p.UnitPrice >= minUnitPrice && p.UnitPrice <=maxUnitPrice);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.nondeleted")]
        public IDataResult<ObjectQueryableDto<Product>> GetAllNonDeleted()
        {
            var result = _productQueryRepository.GetAll(p=>!p.IsDeleted);
            return CheckObjectsReturnValue(result, BusinessMessages.ProductsFoundDueToFilter, BusinessMessages.AnyProductsFoundDueToFilter);
        }

        [SecuredOperationAspect("product.get.id")]
        public IDataResult<ObjectDto<Product>> GetById(int productId)
        {
            var result = _productQueryRepository.Get(p=>p.Id == productId);
            return CheckObjectReturnValue(result, BusinessMessages.ProductFoundDueToFilter, BusinessMessages.AnyProductFoundDueToFilter);
        }

        [SecuredOperationAspect("product.get.stockcode")]
        public IDataResult<ObjectDto<Product>> GetByStockCode(string stockCode)
        {
            var result = _productQueryRepository.Get(p => p.StockCode.Trim().ToLower() == stockCode.Trim().ToLower());
            return CheckObjectReturnValue(result, BusinessMessages.ProductFoundDueToFilter, BusinessMessages.AnyProductFoundDueToFilter);
        }

        [SecuredOperationAspect("product.harddelete")]
        public IResult HardDelete(Product product)
        {
            var logicResult = 
                BusinessLogicEngine.Run
                (CheckIfProductExists(product.Id));
           
            if (logicResult != null)
            {
                return logicResult;
            }

            var entity = _productQueryRepository.Get(p => p.Id == product.Id);

            _productCommandRepository.HardDelete(entity);
            _productCommandRepository.SaveChanges();
            return new SuccessfulResult(BusinessMessages.ProductHardDeleted, BusinessTitles.Successful);
        }

        [SecuredOperationAspect("product.update")]
        public IResult Update(Product product)
        {
            var logicResult =
               BusinessLogicEngine.Run
               (CheckIfProductExists(product.Id));

            if (logicResult != null)
            {
                return logicResult;
            }


            var result = _productCommandRepository.Update(product);
            _productCommandRepository.SaveChanges();

            return CheckObjectReturnValue(result, BusinessMessages.ProductUpdated, BusinessMessages.ProductCouldNotUpdated);
        }




        //Business Utilities

        private IDataResult<ObjectDto<Product>> CheckObjectReturnValue(Product product, string successMessage, string unSuccessMessage)
        {

            if (product != null)
            {
                ObjectDto<Product> objectDto = new ObjectDto<Product>
                {
                    Entity = product,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectDto<Product>>(objectDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectDto<Product>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<Product>> CheckObjectsReturnValue(IQueryable<Product> products, string successMessage, string unSuccessMessage)
        {
            if (products.Count() > -1)
            {
                ObjectQueryableDto<Product> objectQueryableDto = new ObjectQueryableDto<Product>
                {
                    Entities = products,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<Product>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<Product>>(unSuccessMessage, BusinessTitles.Warning);
        }

        private IDataResult<ObjectQueryableDto<T>> CheckObjectsReturnValue<T>(IQueryable<T> entities, string successMessage, string unSuccessMessage)
        where T : class, new()
        {
            if (entities.Count() > -1)
            {
                ObjectQueryableDto<T> objectQueryableDto = new ObjectQueryableDto<T>
                {
                    Entities = entities,
                    ResulStatus = Core.Utilities.Results.ResultStatusEnum.ResultStatusEnum.Successful
                };
                return new SuccessfulDataResult<ObjectQueryableDto<T>>(objectQueryableDto, successMessage, BusinessTitles.Successful);
            }
            else return new UnSuccessfulDataResult<ObjectQueryableDto<T>>(unSuccessMessage, BusinessTitles.Warning);
        }


        //Business Rules

        private IResult CheckIfProductAddedBefore(int categoryId, string name)
        {
            bool status = false;
            var products = this.GetAll();
            foreach (var item in products.Data.Entities)
            {
                if (item.CategoryId == categoryId
                    && item.Name.Trim().ToLower() == name.Trim().ToLower())
                {
                    status = true;
                }
            }

            return status == true
                ? new UnSuccessfulResult(BusinessMessages.ProductExists, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

        private IResult CheckIfProductExists(int productId)
        {
            var products = _productQueryRepository.GetAll();
            foreach (var item in products)
            {
                if (item.Id == productId)
                {
                    return new SuccessfulResult();
                }
            }
            return new UnSuccessfulResult(BusinessMessages.ProductNotFound, BusinessTitles.Warning);
        }

        private IResult CheckIfProductNull(Product product)
        {
            return product == null
                ? new UnSuccessfulResult(BusinessMessages.ProductNotFound, BusinessTitles.Warning)
                : new SuccessfulResult();
        }

    }
}
