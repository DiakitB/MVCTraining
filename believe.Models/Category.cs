using System.ComponentModel.DataAnnotations;

namespace believe.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

