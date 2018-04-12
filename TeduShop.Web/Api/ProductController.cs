using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService, IErrorService errorService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int? categoryId, int page, int pageSize = 20, string filterHotPromotion = "", string keyword = "")
        {
            Func<HttpResponseMessage> func = () =>
            {
                int totalRow = 0;
                IEnumerable<Product> listProductDb = _productService.GetAll(categoryId, filterHotPromotion, keyword);
                totalRow = listProductDb.Count();
                IEnumerable<Product> query = listProductDb.OrderByDescending(x => x.UpdatedDate).Skip(pageSize * (page - 1)).Take(pageSize);
                IEnumerable<ProductViewModel> listProduct = Mapper.Map<IEnumerable<ProductViewModel>>(query);
                PaginationSet<ProductViewModel> pagination = new PaginationSet<ProductViewModel>()
                {
                    TotalRows = totalRow,
                    PageIndex = page,
                    PageSize = pageSize,
                    Items = listProduct,
                };
                return request.CreateResponse(HttpStatusCode.OK, pagination);
            };
            return CreateHttpResponse(request, func);
        }

        [Route("detail/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            Func<HttpResponseMessage> Fuc = () =>
            {
                Product productDb = _productService.GetById(id);
                ProductViewModel productVm = Mapper.Map<ProductViewModel>(productDb);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productVm);
                return response;
            };
            return CreateHttpResponse(request, Fuc);
        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage post(HttpRequestMessage request, ProductViewModel productVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Product productDb = new Product();
                    productDb.UpdateProduct(productVm);
                    productDb.CreateDate = DateTime.Now;
                    productDb.UpdatedDate = DateTime.Now;
                    productDb.CreateBy = User.Identity.Name.ToString();
                    var product = _productService.Add(productDb);
                    _productService.SaveChanges();                   
                    response = request.CreateResponse(HttpStatusCode.Created,productVm);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage put(HttpRequestMessage request, ProductViewModel productVm)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    Product productDb = _productService.GetById(productVm.ID);
                    productDb.UpdateProduct(productVm);
                    productDb.UpdatedDate = DateTime.Now;
                    _productService.Update(productDb);
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, productVm);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
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
                    _productService.Delete(id);
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, id);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
        {
            Func<HttpResponseMessage> Func = () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var productID in listProduct)
                    {
                        _productService.Delete(productID);
                    }
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, listProduct.Count());
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            };
            return CreateHttpResponse(request, Func);
        }
    }
}