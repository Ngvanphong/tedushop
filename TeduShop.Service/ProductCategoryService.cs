using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductCategoryService
    {
        int Add(ProductCategory productCategory);

        void Update(ProductCategory productCategory);

        void Delete(int id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAllByParentId(int parentID);

        ProductCategory GetById(int id);

        IEnumerable<ProductCategory> GetAllByAlias(string alias);
        void SaveChanges();
        
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository)
        {
            this._unitOfWork = unitOfWork;
            this._productCategoryRepository = productCategoryRepository;
        }

        public int Add(ProductCategory productCategory)
        {
            var query = _productCategoryRepository.Add(productCategory);
            return query.ID;
        }

        public void Delete(int id)
        {
            _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllByAlias(string alias)
        {
            return _productCategoryRepository.GetByAlias(alias);
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentID)
        {
            return _productCategoryRepository.GetMulti(x => x.ParentID == parentID && x.Status);
        }

        public ProductCategory GetById(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }

        public void Update(ProductCategory productCategory)
        {
            _productCategoryRepository.Update(productCategory);
        }
        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }
    }
}