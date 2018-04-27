using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int productId;
        public ProductViewModel productViewModel;
        public int Quantity;

    }
}