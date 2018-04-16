using System;

using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
  
    public class PostImageViewModel
    {


        public int ID { get; set; }

        public int PostId { get; set; }

        public virtual PostViewModel Post { get; set; }

        public string Path { get; set; }

        public string Caption { get; set; }

    }
}