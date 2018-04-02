using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;


        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, int page, int pageSize=20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRows = 0;
                var listProductCategoryDb = _productCategoryService.GetAll();
                totalRows = listProductCategoryDb.Count();
                var query = listProductCategoryDb.OrderByDescending(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize);
                var listProductCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(query);
                PaginationSet<ProductCategoryViewModel> pagination = new PaginationSet<ProductCategoryViewModel>()
                {
                    TotalRows = totalRows,
                    PageIndex = page,
                    PageSize = pageSize,              
                    Items = listProductCategoryVm,

                };
                             
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK,pagination);

                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var newProductCategoryDb = new ProductCategory();
                    newProductCategoryDb.UpdateProductCategory(productCategoryVm);
                     var productCategory= _productCategoryService.Add(newProductCategoryDb);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, productCategory);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage put(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    ProductCategory productCategory = _productCategoryService.GetById(productCategoryVm.ID);
                    productCategory.UpdateProductCategory(productCategoryVm);
                    _productCategoryService.Update(productCategory);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request,int id)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
            
        }
    }
}