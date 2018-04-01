using AutoMapper;
using System.Collections.Generic;
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
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService, IErrorService errorService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        public HttpResponseMessage get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                IEnumerable<Product> listProductDb = _productService.GetAll();
                List<ProductViewModel> listProductVm = Mapper.Map<List<ProductViewModel>>(listProductDb);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Created, listProductVm);
                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage post(HttpRequestMessage request, ProductViewModel productVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Product productDb = new Product();
                    productDb.UpdateProduct(productVm);
                    var product = _productService.Add(productDb);
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, product);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            });
        }
        [Route("update")]
        public HttpResponseMessage put(HttpRequestMessage request,ProductViewModel productVm)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    Product productDb = _productService.GetById(productVm.ID);
                    productDb.UpdateProduct(productVm);
                    _productService.Update(productDb);
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
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
                    _productService.Delete(id);
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            });
        }
    }
}