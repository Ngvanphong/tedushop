using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { set; get; }
        public string Type { set; get; }
        public virtual IEnumerable<ProductTag> ProductTag { get; set; }
        public virtual IEnumerable<PostTag> PostTag { get; set; }


    }
}
