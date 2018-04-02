﻿using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService
    {
        int Add(Product product);

        void Update(Product product);

        void Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(int? categoryId, string keywords);

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow);

        Product GetById(int id);

        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }

        public int Add(Product product)
        {
            Product query = _productRepository.Add(product);
            return query.ID;
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll(new string[] { "ProductCategory", "ProductTag" });
        }

        public IEnumerable<Product> GetAll(int? categoryId, string keywords)
        {
            var query = _productRepository.GetAll(new string[] { "ProductCategory", "ProductTag" });
            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(x => (x.Name.Contains(keywords) || x.Alias.Contains(keywords) && x.Status));
            };
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryID == categoryId);
            };
            return query;
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
        }
    }
}