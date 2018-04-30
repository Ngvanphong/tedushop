﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
       private IProductService _productService;
       private IProductQuantityService _productQantityService;
        public ShoppingCartController(IProductService productService, IProductQuantityService productQuantityService)
        {
            this._productService = productService;
            this._productQantityService = productQuantityService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        { 
            if (Session[Common.CommonConstant.SesstionCart] == null)
            {
                Session[Common.CommonConstant.SesstionCart] = new List<ShoppingCartViewModel>();
            }
            
            return View();
        }
        
        public JsonResult GetAll()
        {
            if (Session[Common.CommonConstant.SesstionCart] == null)
            {
                Session[Common.CommonConstant.SesstionCart] = new List<ShoppingCartViewModel>();
            }
            
            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstant.SesstionCart];
            return Json(new
            {
                status=true,
                data=cart
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productId,int sizeId)
        {

            var shoppingCart = (List<ShoppingCartViewModel>)Session[Common.CommonConstant.SesstionCart];
            SizeViewModel sizeVm = new SizeViewModel();

               sizeVm = Mapper.Map<SizeViewModel>(_productQantityService.GetSizeById(sizeId));

                                      
            if (shoppingCart == null)
            {
                shoppingCart = new List<ShoppingCartViewModel>();
            };
            if (shoppingCart.Any(x => x.productId == productId&&x.SizesVm.ID==sizeId))
            {
                foreach(var item in shoppingCart)
                {

                    if (item.productId == productId&&item.SizesVm.ID==sizeId)
                    {
                        item.Quantity += 1;
                       
                    }
                }
            }
            else
            {
                Product product = _productService.GetById(productId);

                ShoppingCartViewModel cart = new ShoppingCartViewModel()
                {
                    productId = productId,
                    productViewModel = Mapper.Map<ProductViewModel>(product),
                    Quantity = 1,                  
                    SizesVm= sizeVm,
                };
                shoppingCart.Add(cart);
            }
            Session[Common.CommonConstant.SesstionCart] = shoppingCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult Update(string listCart)
        {
            var listCartVm = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(listCart);
            var listCartSession = (List<ShoppingCartViewModel>)Session[Common.CommonConstant.SesstionCart];
            foreach (var item in listCartSession)
            {
                foreach(var itemVm in listCartVm)
                {
                    if (itemVm.productId == item.productId)
                    {
                        item.Quantity = itemVm.Quantity;
                    }
                }

            }
            Session[Common.CommonConstant.SesstionCart] = listCartSession;

            return Json(new
            {
                status = true
            });

        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[Common.CommonConstant.SesstionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status=true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId, string size)
        {     
            var shoppingCart = (List<ShoppingCartViewModel>)Session[Common.CommonConstant.SesstionCart];
            if (shoppingCart != null)
            {
                shoppingCart.RemoveAll(x => x.productId == productId&&(x.SizesVm.Name==size|| x.SizesVm.Name == null));
            }
            
            Session[Common.CommonConstant.SesstionCart] = shoppingCart;
            return Json(new
            {
                status = true,            

            });

        }
       
    }
}