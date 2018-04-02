using System.Collections.Generic;

namespace TeduShop.Web.Models
{
    public class MenuGroupViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public virtual List<MenuViewModel> Menu { get; set; }
    }
}