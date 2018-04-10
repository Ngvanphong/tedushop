using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        void Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(int? categoryId, string keyword);
        

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow);

        Product GetById(int id);
        IEnumerable<Product> GetHotProduct();

        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;
        private ITagRepository _tagReponsitory;
        private IProductTagRepository _productTagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ITagRepository tagRepository, IProductTagRepository productTagRepository)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._tagReponsitory = tagRepository;
            this._productTagRepository = productTagRepository;
        }

        public Product Add(Product product)
        {
            Product query = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] listTag = product.Tags.Split(',');
                for (int i = 0; i < listTag.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(listTag[i]);
                    if (_tagReponsitory.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = listTag[i],
                            Type = CommonConstant.ProductTag,
                        };
                        _tagReponsitory.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = query.ID,
                        TagID = tagId,
                    };
                    _productTagRepository.Add(productTag);
                }
            }
            return query;
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll(new string[] { "ProductCategory", "ProductTag" });
        }

        public IEnumerable<Product> GetAll(int? categoryId, string keyword)
        {
            var query = _productRepository.GetAll(new string[] { "ProductCategory", "ProductTag" });
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.Name.Contains(keyword) || x.Alias.Contains(keyword) && x.Status));
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

        public IEnumerable<Product> GetHotProduct()
        {
            IEnumerable<Product> listHotProduct = _productRepository.GetMulti(x => x.Status && x.HotFlag == true)
                .OrderByDescending(x => x.CreateDate);
            return listHotProduct;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] listTags = product.Tags.Split(',');
                foreach (var item in listTags)
                {
                    var tagId = StringHelper.ToUnsignString(item);
                    if (_tagReponsitory.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = item,
                            Type = CommonConstant.ProductTag,
                        };
                        _tagReponsitory.Add(tag);
                    }
                    int coutProductTag = _productTagRepository.GetMulti(x => (x.ProductID == product.ID && x.TagID == tagId)).Count();
                    if (coutProductTag == 0)
                    {
                        ProductTag productTag = new ProductTag()
                        {
                            ProductID = product.ID,
                            TagID = tagId,
                        };
                        _productTagRepository.Add(productTag);
                    }
                }
            }
        }
    }
}