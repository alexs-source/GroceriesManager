using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Item : ObjectBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}