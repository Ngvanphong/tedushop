using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IPostCategoryService _postCategoryService;

        private IProductService _productService;
        private IPostService _postService;
        private ISlideService _slideService;

        public HomeController(IProductCategoryService productCatgoryService, IPostCategoryService postCategoryService,
            IProductService productService, IPostService postService,ISlideService slideService)
        {
            this._productCategoryService = productCatgoryService;
            this._postCategoryService = postCategoryService;
            this._postService = postService;
            this._productService = productService;
            this._slideService = slideService;
        }
        public ActionResult Index()
        {
            IndexViewModel indexVm = new IndexViewModel()
            {

            };
            IEnumerable<Product> listHotProductDb = _productService.GetAll().Where(x => x.HotFlag == true && x.HotFlag == true).OrderByDescending(x => x.UpdatedDate).Take(7);
            IEnumerable<ProductViewModel> listHotProductVm = Mapper.Map<IEnumerable<ProductViewModel>>(listHotProductDb);
            indexVm.productHotVm = listHotProductVm;
            IEnumerable<Product> listPromotionProductDb = _productService.GetAll().Where(x => x.HotFlag == true && x.PromotionPrice.HasValue).OrderByDescending(x=>x.UpdatedDate).Take(7);
            IEnumerable<ProductViewModel> listPromotionVm = Mapper.Map<IEnumerable<ProductViewModel>>(listPromotionProductDb);
            indexVm.productPromotionVm = listPromotionVm;

            IEnumerable<Slide> listSlideDb = _slideService.GetAll();
            IEnumerable<SlideViewModel> listSlideVm = Mapper.Map<IEnumerable<SlideViewModel>>(listSlideDb);
            indexVm.slideVm = listSlideVm;

            IEnumerable<Post> listPostDb = _postService.GetAll().Where(x => x.HomeFlag == true).OrderByDescending(x => x.UpdatedDate).Take(3);
            IEnumerable<PostViewModel> listPostVm = Mapper.Map<IEnumerable<PostViewModel>>(listPostDb);
            indexVm.postVm = listPostVm;

            return View(indexVm);
        }
        
        [ChildActionOnly]
        public ActionResult Footer()
        {

            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            HeaderViewModel headerVm =new HeaderViewModel()
            {

            };
        
            IEnumerable<ProductCategory> productCategoryDb = _productCategoryService.GetAll();
            IEnumerable<ProductCategoryViewModel> productCategoryVm = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategoryDb);
            IEnumerable<PostCategory> postCategoryDb = _postCategoryService.GetAll();
            IEnumerable<PostCategoryViewModel> postCategoryVm = Mapper.Map < IEnumerable<PostCategoryViewModel>>(postCategoryDb);

            headerVm.productCategoryVm = productCategoryVm;
            headerVm.postCategoryVm = postCategoryVm;

            return PartialView(headerVm);
        }

    }
}