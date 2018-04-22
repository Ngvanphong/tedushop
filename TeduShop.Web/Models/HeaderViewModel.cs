using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class HeaderViewModel
    {
        public IEnumerable<ProductCategoryViewModel> productCategoryVm;

        public IEnumerable<PostCategoryViewModel> postCategoryVm;
    }
}