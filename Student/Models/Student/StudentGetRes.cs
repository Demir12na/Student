namespace StudentManagementSystem.Models.Student
{
    public class StudentGetRes
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public decimal? AcademicPerformance { get; set; }
        public string GroupName { get; set; }
        public string DepartmentName { get; set; }
    }
}
