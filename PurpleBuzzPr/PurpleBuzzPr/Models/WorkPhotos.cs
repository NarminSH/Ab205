using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzPr.Models
{
    public class WorkPhotos
    {
        public int Id { get; set; }
        public int WorkId { get;set; }
        public Work? Work { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string ImageUrl { get; set; }
     
    }
}
