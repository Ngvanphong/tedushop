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
    [RoutePrefix("api/post")]
    [Authorize]
    public class PostController : ApiControllerBase
    {
        private IPostService _postService;

        public PostController(IErrorService errorService, IPostService postService) : base(errorService)
        {
            this._postService = postService;
        }

        [Route("getall")]
        public HttpResponseMessage get(HttpRequestMessage request, int page, int pageSize=10)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRows = 0;
                IEnumerable<Post> listPostDb = _postService.GetAll();
                totalRows = listPostDb.Count();
                listPostDb = listPostDb.OrderByDescending(x => x.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize);
                IEnumerable<PostViewModel> listPostVm = Mapper.Map<List<PostViewModel>>(listPostDb);
                PaginationSet<PostViewModel> pagination = new PaginationSet<PostViewModel>()
                {
                    PageIndex = page,
                    PageSize=pageSize,
                    TotalRows=totalRows,
                    Items=listPostVm,                   
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, pagination);
                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage post(HttpRequestMessage request, PostViewModel postVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Post postDb = new Post();
                    postDb.UpdatePost(postVm);
                    var post = _postService.Add(postDb);
                    _postService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, post);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage put(HttpRequestMessage request, PostViewModel postVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Post postDb = _postService.GetById(postVm.ID);
                    postDb.UpdatePost(postVm);
                    _postService.Update(postDb);
                    _postService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            });
        }

        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    _postService.Delete(id);
                    _postService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, id);
                }
                else
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            });
        }
    }
}