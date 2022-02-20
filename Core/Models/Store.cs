using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Store : ObjectBase
    {
        [Key]
        public int StoreId { get; set; }
        
        [StringLength(50)]
        public string Purpose { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}