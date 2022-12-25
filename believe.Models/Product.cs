using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace believe.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(0, 100)]
        public double ListPrice { get; set; }
        [Required]
		[DisplayName("Price for 05")]
		public double Price50 { get; set; }
        [Required]
        [Range(0, 100)]
		[DisplayName("Price for 75")]

		public double Price75 { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
		[DisplayName("Category")]
		public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
		[DisplayName("Category")]
		public Category Category { get; set; }
        [Required]
		[DisplayName("Cover Type")]

		public int CoverTypeId { get; set; }
        [ValidateNever]
        [DisplayName("Cover Type")]
        public CoverType CoverType { get; set; }

    }
}
