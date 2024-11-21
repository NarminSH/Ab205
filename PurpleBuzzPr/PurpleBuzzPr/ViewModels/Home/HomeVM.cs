using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Work> Works { get; set; }
    }
}
