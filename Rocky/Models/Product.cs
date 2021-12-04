using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Models
{
    public class Product
    {
        [key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public double Price { get; set; }

        public string Image { get; set; }

        // to add foraign key raltion

        [Display(Name = "Category Type")]
        public  int CategoryId { get; set; } // this property is the mapping entity between Product and Catagory and we will pass it as foraign key

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
