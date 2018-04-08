using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [Authorize]
    [RoutePrefix("api/function")]
    public class FunctionController : ApiControllerBase
    {
        private IFunctionService _functionService;

        public FunctionController(IErrorService errorService, IFunctionService functionService) : base(errorService)
        {
            this._functionService = functionService;
        }

        [Route("getlisthierarchy")]
        [HttpGet]
        public HttpResponseMessage GetAllHierachy(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<Function> model;
                if (User.IsInRole("Admin"))
                {
                   model = _functionService.GetAll(string.Empty);
                }
                else
                {
                  model = _functionService.GetAllWithPermission(User.Identity.GetUserId());
                }

                IEnumerable<FunctionViewModel> modelVm = Mapper.Map<IEnumerable<FunctionViewModel>>(model);
                var parents = modelVm.Where(x => x.Parent == null);
                foreach (var parent in parents)
                {
                    parent.ChildFunctions = modelVm.Where(x => x.ParentId == parent.ID).ToList();
                }
                response = request.CreateResponse(HttpStatusCode.OK, parents);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string filter = "")
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _functionService.GetAll(filter);

                IEnumerable<FunctionViewModel> modelVm = Mapper.Map<IEnumerable<Function>, IEnumerable<FunctionViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }
    }
}