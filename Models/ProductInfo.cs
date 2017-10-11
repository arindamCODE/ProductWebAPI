using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProductInfo
    {
        public int ID{get; set;}

        public int groupID{get; set;}

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public int Rate { get; set; }
    }
}