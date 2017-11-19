using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("PostTags")]
  public  class PostTag
    {
        [Key]
        public int PostID { get; set; }
        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }
        [Key]
        public string TagID { get; set; }
        [ForeignKey("TagID")]
        public virtual Tag Tag { set; get; }
    }
}
