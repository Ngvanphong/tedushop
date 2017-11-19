using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string CustomerName { set; get; }
        [Required]
        public string CustomerAddress { set; get; }
        [Required]
        public string CustomerMobile { set; get; }
        [Required]
        public string CustomerEmail { set; get; }
        public string CustomerMessage { set; get; }
        [Required]
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { set; get; }
        public bool Status { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetail { set; get; }
        
    }
}
