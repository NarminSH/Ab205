namespace PurpleBuzzPr.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string Profession { get; set; }
        public ICollection<EmployeeWork>? EmployeeWorks { get; set; }


    }
}
