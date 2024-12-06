using PurpleBuzzPr.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PurpleBuzzPr.DTOs.WorkDTOs
{
    public class UpdateWorkDTO
    {
        [MinLength(3), DisallowNull]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string MainImageUrl { get; set; }
        public int ServiceId { get; set; }
        public List<int> EmployeeIds { get; set; }
        public List<WorkPhotos>? AdditionalPhotos { get; set; }
        public IFormFile MainImage { get; set; }
        public List<IFormFile> AdditionalImages { get; set; }


    }
}
