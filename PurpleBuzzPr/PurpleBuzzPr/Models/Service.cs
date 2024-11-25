using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PurpleBuzzPr.Models
{
    public class Service
    {
        public int Id { get; set; }

        [MinLength(3), DisallowNull]
        public string Title { get; set; }

        public string Description { get; set; }
        [AllowNull]
        public string? MainImageUrl { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public ICollection<Work>? Works { get; set; }
    }
}
