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
using TeduShop.Web.Models;
using TeduShop.Web.Providers;

namespace TeduShop.Web.Api
{
    [Authorize]
    [RoutePrefix("api/appUser")]
    public class AppUserController : ApiControllerBase
    {
        public AppUserController(IErrorService errorService) : base(errorService)
        {
        }

        [Route("getlistpaging")]
        [HttpGet]
        [Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            Func<HttpResponseMessage> func = () =>
            {
                HttpResponseMessage response = null;
                int totalRows = 0;
                var listAppUserDb = AppUserManager.Users;
                if (!string.IsNullOrEmpty(filter))
                    listAppUserDb = listAppUserDb.Where(x => x.UserName.Contains(filter) || x.FullName.Contains(filter));
                totalRows = listAppUserDb.Count();
                var query = listAppUserDb.OrderBy(x => x.UserName).Skip((page - 1) * pageSize).Take(pageSize);
                IEnumerable<ApplicationUserViewModel> listApplicationUserVm = Mapper.Map<IEnumerable<ApplicationUserViewModel>>(query);
                PaginationSet<ApplicationUserViewModel> pagination = new PaginationSet<ApplicationUserViewModel>()
                {
                    PageIndex = page,
                    PageSize = pageSize,
                    TotalRows = totalRows,
                    Items = listApplicationUserVm,
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagination);
                return response;
            };
            return CreateHttpResponse(request, func);
        }
    }
}