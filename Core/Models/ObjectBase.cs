using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class ObjectBase
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}