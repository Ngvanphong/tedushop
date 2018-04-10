using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/productCategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService, IProductService productService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string filter="")
        {
            return CreateHttpResponse(request, () =>
             {
                 IEnumerable<ProductCategory> listProductCategoryDb = _productCategoryService.GetAll(filter);
                 IEnumerable<ProductCategoryViewModel> listProdutCategoryVm = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(listProductCategoryDb);
                 return request.CreateResponse(HttpStatusCode.OK, listProdutCategoryVm);
             });
        }

        [Route("getallhierachy")]
        [HttpGet]
        public HttpResponseMessage GetAllHierachy(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {

                IEnumerable<ProductCategory> listProductCategoryDb = _productCategoryService.GetAll().ToList();
                IEnumerable<ProductCategoryViewModel> listProductCategoryVm = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(listProductCategoryDb);

                IEnumerable<ProductCategoryViewModel> listParent = listProductCategoryVm.Where(x => x.ParentID == null);
                
                foreach (var parent in listParent)
                {                    
                    parent.ChildFunctions = listProductCategoryVm.Where(x => x.ParentID == parent.ID).ToList();
                }
                return request.CreateResponse(HttpStatusCode.OK, listParent);
            };
            return CreateHttpResponse(request, func);

        }
        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var newProductCategoryDb = new ProductCategory();
                    newProductCategoryDb.UpdateProductCategory(productCategoryVm);
                    newProductCategoryDb.CreateDate = DateTime.Now;
                    _productCategoryService.Add(newProductCategoryDb);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created,productCategoryVm);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    ProductCategory productCategory = _productCategoryService.GetById(productCategoryVm.ID);
                    productCategory.UpdateProductCategory(productCategoryVm);
                    productCategory.UpdatedDate = DateTime.Now;
                    _productCategoryService.Update(productCategory);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created,productCategoryVm);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {                 
                    _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK,id);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }
        [Route("detail/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                ProductCategory productCategory = _productCategoryService.GetById(id);
                ProductCategoryViewModel productCategoryVm = Mapper.Map<ProductCategoryViewModel>(productCategory);
                return request.CreateResponse(HttpStatusCode.OK, productCategoryVm);
            });
        }
    }
}