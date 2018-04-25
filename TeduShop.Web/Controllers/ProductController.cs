using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }
        // GET: ProductCategory
        public ActionResult Index(int id, int page = 1, string sort = "")
        {
            ProductCategory category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategoryViewModel>(category);
            ViewBag.Sort = sort;
            int pageSize = Common.CommonConstant.PageSize;
            int totalRow = 0;
            IEnumerable<Product> listProductDb = _productService.GetAllByCategoryPaging(id, page, pageSize, sort, out totalRow);
            IEnumerable<ProductViewModel> listProductVm = Mapper.Map<IEnumerable<ProductViewModel>>(listProductDb);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            PaginationClient<ProductViewModel> pagination = new PaginationClient<ProductViewModel>()
            {
                TotalPage = totalPage,
                TotalRows = totalRow,
                PageDisplay = Common.CommonConstant.PageDisplay,
                Items= listProductVm,
                PageIndex=page,
                PageSize=pageSize,

            };


            return View(pagination);
        } 
        public JsonResult GetListProductByName(string prodcuctName)
        {
           IEnumerable<string> listProductName = _productService.GetProductName(prodcuctName);
            return Json(new
            {
                data = listProductName
            },JsonRequestBehavior.AllowGet);
                
            
        }

        public ActionResult SearchProduct(string productName, int page = 1, string sort="")
        {
            int pageSize = Common.CommonConstant.PageSize;
            int totalRow = 0;
            ViewBag.Sort = sort;
            ViewBag.ProductName = productName;
            IEnumerable<Product> listProductDb = _productService.GetAllByNamePaging(productName, page, pageSize, sort, out totalRow);
            IEnumerable<ProductViewModel> listProductVm = Mapper.Map<IEnumerable<ProductViewModel>>(listProductDb);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            PaginationClient<ProductViewModel> pagination = new PaginationClient<ProductViewModel>()
            {
                PageDisplay=Common.CommonConstant.PageDisplay,
                PageIndex=page,
                PageSize=pageSize,
                TotalPage=totalPage,
                Items=listProductVm,
                TotalRows=totalRow,
            };
            return View(pagination);
        }
    }
}