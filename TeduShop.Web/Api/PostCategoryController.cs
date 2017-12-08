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

    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        
        private IPostCategoryService _postCategorySevice;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategorySevice = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
           
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _postCategorySevice.GetAll();
                var lisatCategoryVm = Mapper.Map<List<PostCategory>>(listCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, lisatCategoryVm);

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

                    var category = _postCategorySevice.Add(newpostCategory);
                    _postCategorySevice.SaveChange();
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
                    var postCategoryDb = _postCategorySevice.GetByID(postCategoryVm.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVm);
                    _postCategorySevice.Update(postCategoryDb);
                    _postCategorySevice.SaveChange();
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
                    _postCategorySevice.Delete(id);
                    _postCategorySevice.SaveChange();
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