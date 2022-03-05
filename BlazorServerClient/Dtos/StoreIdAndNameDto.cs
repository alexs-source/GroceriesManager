using System.ComponentModel.DataAnnotations;

namespace BlazorServerClient.Dtos
{
    public class StoreIdAndNameDto
    {
        public int StoreId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}