﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ProductDetailViewModel
    {
        public ProductViewModel ProductVm;
        public IEnumerable<ProductViewModel> ListProductVm;
        public IEnumerable<ProductImageViewModel> ListProductImageVm;
    }
}