using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class VisitorStatisticViewModel
    {

        public Guid ID { set; get; }
        public DateTime VisitorDate { get; set; }

        public string IPAddress { set; get; }
    }
}