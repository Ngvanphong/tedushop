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
    [Table("ProductCategories")]
    public class ProductCategory:TableCommon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Alias { get; set; }
        public int? ParentID { get; set; }
        public string Description { set; get; }
        public int? DisplayOrder { get; set; }
        public string Image { get; set; }
        public bool? HomeFlag { set; get; }
        public virtual IEnumerable<Product> Product { set; get; }

    }
}