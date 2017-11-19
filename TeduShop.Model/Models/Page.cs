using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("Pages")]
   public class Page
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Content { set; get; }
        [MaxLength(50)]
        public string MetaKeyword { set; get; }
       [MaxLength(256)]
        public string MetaDescription { set; get; }
        [Required]
        public bool Status { get; set; }

    }
}
