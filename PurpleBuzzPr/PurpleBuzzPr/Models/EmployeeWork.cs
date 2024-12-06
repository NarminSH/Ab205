namespace PurpleBuzzPr.Models
{
    public class EmployeeWork
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int WorkId { get; set; }
        public Employee? Employee { get; set; }
        public Work? Work { get; set; }
    }
}
