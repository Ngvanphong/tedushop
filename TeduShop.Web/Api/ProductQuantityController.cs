using AutoMapper;
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
    [RoutePrefix("api/productQuantity")]
    public class ProductQuantityController : ApiControllerBase
    {
        private IProductQuantityService _productQuantityService;

        public ProductQuantityController(IErrorService errorService, IProductQuantityService productQuantityService) : base(errorService)
        {
            this._productQuantityService = productQuantityService;
        }

        [Route("getsizes")]
        [HttpGet]
        public HttpResponseMessage GetSizes(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                IEnumerable<Size> listSizeDb = _productQuantityService.GetListSize();
                IEnumerable<SizeViewModel> listSizeVm = Mapper.Map<IEnumerable<SizeViewModel>>(listSizeDb);
                listSizeVm = listSizeVm.OrderBy(x => x.Name);
                return request.CreateResponse(HttpStatusCode.OK, listSizeVm);
            });
        }

        [Route("sizesdetail/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetSizes(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var sizeDb = _productQuantityService.GetSizeById(id);
                var sizeVm = Mapper.Map<SizeViewModel>(sizeDb);
                return request.CreateResponse(HttpStatusCode.OK, sizeVm);
            });
        }
        [Route("addsizes")]
        [HttpPost]
        public HttpResponseMessage AddSizes(HttpRequestMessage request, SizeViewModel sizeVm)
        {
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    Size sizeDb = new Size();
                    sizeDb.UpdateSize(sizeVm);
                    _productQuantityService.AddSize(sizeDb);
                    _productQuantityService.SaveChange();
                    return request.CreateResponse(HttpStatusCode.OK, sizeVm);
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                }
            });
        }

        [Route("updatesizes")]
        [HttpPut]
        public HttpResponseMessage UpdateSizes(HttpRequestMessage request, SizeViewModel sizeVm)
        {
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var sizeDb = _productQuantityService.GetSizeById(sizeVm.ID);
                    sizeDb.UpdateSize(sizeVm);
                    _productQuantityService.UpdateSize(sizeDb);
                    _productQuantityService.SaveChange();
                    return request.CreateResponse(HttpStatusCode.OK, sizeVm);
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                }
            });
        }

        [Route("deletesize")]
        [HttpDelete]
        public HttpResponseMessage DeleteSizes(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    _productQuantityService.DeleteSize(id);
                    _productQuantityService.SaveChange();
                    return request.CreateResponse(HttpStatusCode.OK, id);
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            });
        }
    }
}