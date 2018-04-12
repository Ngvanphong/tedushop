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
    [Authorize]
    [RoutePrefix("api/productImage")]
    public class ProductImageController : ApiControllerBase
    {
        IProductImageService _productImageService;
        public ProductImageController(IErrorService errorService, IProductImageService productImageService) : base(errorService)
        {
            this._productImageService = productImageService;
            
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                IEnumerable<ProductImage> productImageDb = _productImageService.GetAll(productId);
                IEnumerable<ProductImageViewModel> productImageVm = Mapper.Map<IEnumerable<ProductImageViewModel>>(productImageDb);
                return request.CreateResponse(HttpStatusCode.OK, productImageVm);
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductImageViewModel productImageVm)
        {
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    ProductImage productImageDb = new ProductImage();
                    productImageDb.UpdateProductImage(productImageVm);
                    _productImageService.Add(productImageDb);                 
                    _productImageService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, productImageVm);
                }
                else
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                
            });

        }
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                _productImageService.Delete(id);
                _productImageService.Save();
                return request.CreateResponse(HttpStatusCode.OK, id);
            });
        }
    }
}
