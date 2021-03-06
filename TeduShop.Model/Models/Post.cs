﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("Posts")]
    
   public class Post:TableCommon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Alias { set; get; }
        [Required]
        public int CategoryID { set; get; }
        [ForeignKey("CategoryID")]
        public virtual PostCategory PostCategory { get; set; }
        public int? DisplayOrder { get; set; }
        [MaxLength(256)]
        public string Description { set; get; }
        public string Content { set; get; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }
        public virtual IEnumerable<PostTag> PostTag { get; set; }
    }
}
