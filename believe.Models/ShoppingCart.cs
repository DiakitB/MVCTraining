using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace believe.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        [Range(1, 100, ErrorMessage ="please enter a number betweeen 1, and 1000")]
        public int Count { get; set; }
    }
}
