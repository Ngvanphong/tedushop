using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ProductViewModel
    {
      
        public int ID { get; set; }
     
        public string Name { set; get; }
      
        public string Alias { set; get; }

        public int CategoryID { set; get; }
        public virtual ProductCategoryViewModel ProductCategory { set; get; }
        public int? DisplayOrder { set; get; }

        public string Image { set; get; }

        public string MoreImage { set; get; }
        public Decimal Price { get; set; }
        public Decimal? PromotionPrice { set; get; }
        public int? Warranty { set; get; }
 
        public string Description { set; get; }
        public string Content { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { set; get; }

        public string MetaKeyword { get; set; }

        public string MetaDiscription { get; set; }
        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public bool Status { get; set; }

        public virtual List<OrderDetailViewModel> OrderDetail { get; set; }
        public virtual List<ProductTagViewModel> ProductTag { set; get; }
        public string Tags { set; get; }
    }
}