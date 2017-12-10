using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._productService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {

            return CreateHttpResponse(request, () =>
            {
                var listCategory = _productService.GetAll();
                var listCategoryVm = Mapper.Map<List<PostCategory>>(listCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategoryVm);

                return response;
            });
        }
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    PostCategory newpostCategory = new PostCategory();
                    newpostCategory.UpdatePostCategory(postCategoryVm);

                    var category = _productService.Add(newpostCategory);
                    _productService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                else
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var postCategoryDb = _productService.GetByID(postCategoryVm.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVm);
                    _productService.Update(postCategoryDb);
                    _productService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    _productService.Delete(id);
                    _productService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

    }
}
