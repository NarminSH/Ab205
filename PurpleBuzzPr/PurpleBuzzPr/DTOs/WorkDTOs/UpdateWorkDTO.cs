using PurpleBuzzPr.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PurpleBuzzPr.DTOs.WorkDTOs
{

  public class UpdateWorkDTO
    {
        public int Id { get; set; }
        [MinLength(3), DisallowNull]
        public string Title { get; set; }
        public string Description { get; set; }
 
        public string? ExistingMainImageUrl { get; set; }

        public List<WorkPhotos>? ExistingPhotos { get; set; }
        public IFormFile? NewMainImage { get; set; }
        public List<IFormFile>? NewPhotos { get; set; }
    }
}
