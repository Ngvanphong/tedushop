using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("SupportOnlines")]
  public  class SupportOnline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [Required]
        public string Name { get; set; }
        public string Department { get; set; }
        public string Skype { set; get; }
        public string Email { set; get; }
        public string Mobile { set; get; }
        public string Yahoo { set; get; }
        public string Facebook { set; get; }
        [Required]
        public string Status { get; set; }
    }
}
