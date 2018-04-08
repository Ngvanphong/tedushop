using AutoMapper;
using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/appRole")]
    [Authorize]
    public class AppRoleController : ApiControllerBase
    {
        private IFunctionService _functionService;
        private IPermissionService _permissionService;

        public AppRoleController(IErrorService errorService, IFunctionService functionService, IPermissionService permissionService) : base(errorService)
        {
            this._functionService = functionService;
            this._permissionService = permissionService;
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            Func<HttpResponseMessage> func = () =>
            {
                HttpResponseMessage response = null;
                int totalRows = 0;
                var query = AppRoleManager.Roles;
                if (!string.IsNullOrEmpty(filter))
                    query = query.Where(x => x.Description.Contains(filter));
                totalRows = query.Count();
                var model = query.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize);
                IEnumerable<ApplicationRoleViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationRoleViewModel>>(model);
                PaginationSet<ApplicationRoleViewModel> pagination = new PaginationSet<ApplicationRoleViewModel>
                {
                    PageIndex = page,
                    TotalRows = totalRows,
                    PageSize = pageSize,
                    Items = modelVm,
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagination);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("detail/{id}")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AppRole roleDb = AppRoleManager.FindById(id);
                ApplicationRoleViewModel roleVm = Mapper.Map<ApplicationRoleViewModel>(roleDb);
                response = request.CreateResponse(HttpStatusCode.OK, roleVm);
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AppRole role = AppRoleManager.FindById(id);
                AppRoleManager.Delete(role);
                response = request.CreateResponse(HttpStatusCode.OK, id);
                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    AppRole newRoleDb = new AppRole();
                    newRoleDb.UpdateApplicationRole(applicationRoleViewModel, "add");
                    AppRoleManager.Create(newRoleDb);
                    response = request.CreateResponse(HttpStatusCode.Created, newRoleDb);
                }

                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    AppRole roleDb = AppRoleManager.FindById(applicationRoleViewModel.Id);
                    roleDb.UpdateApplicationRole(applicationRoleViewModel, "update");
                    AppRoleManager.Update(roleDb);
                    response = request.CreateResponse(HttpStatusCode.Created, roleDb);
                }
                return response;
            });
        }

        [Route("getAllPermission")]
        [HttpGet]
        public HttpResponseMessage GetAllPermission(HttpRequestMessage request, string functionId)
        {
            Func<HttpResponseMessage> func = () =>
            {
                HttpResponseMessage response = null;
                List<PermissionViewModel> permisstions = new List<PermissionViewModel>();
                List<AppRole> roles = AppRoleManager.Roles.Where(r => r.Name != "Admin").ToList();
                var listPermission = _permissionService.GetByFunctionId(functionId);
                if (listPermission.Count == 0)
                {
                    foreach (var item in roles)
                    {
                        permisstions.Add(new PermissionViewModel()
                        {
                            RoleId = item.Id,
                            CanCreate = false,
                            CanUpdate = false,
                            CanDelete = false,
                            CanRead = false,
                            AppRole = new ApplicationRoleViewModel()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Description = item.Description,
                            }
                        });
                    }
                }
                else
                {
                    foreach (var item in roles)
                    {
                        if (!listPermission.Any(x => x.FunctionId == item.Id))
                        {
                            permisstions.Add(new PermissionViewModel()
                            {
                                RoleId = item.Id,
                                CanCreate = false,
                                CanUpdate = false,
                                CanDelete = false,
                                CanRead = false,
                                AppRole = new ApplicationRoleViewModel()
                                {
                                    Id = item.Id,
                                    Name = item.Name,
                                    Description = item.Description,
                                }
                            });
                        }
                        permisstions = Mapper.Map<List<PermissionViewModel>>(listPermission);
                    }
                };
                response = request.CreateResponse(HttpStatusCode.OK, permisstions);
                return response;
            };
            return CreateHttpResponse(request, func);
        }
    }
}