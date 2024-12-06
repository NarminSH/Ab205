using PurpleBuzzPr.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PurpleBuzzPr.DTOs.WorkDTOs
{
    public class CreateWorkDTO
    {
        [MinLength(3), DisallowNull]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile MainImageUrl { get; set; }
        public int ServiceId { get; set; }
        public List<int> EmployeeIds { get; set; }
        public List<IFormFile>? Images { get; set; }
       

       



    }
}
