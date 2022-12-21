using StudentManagementSystem.Entities;

namespace StudentManagementSystem.Models.Student
{
    public class StudentGetGroupByStudentIdRes
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string GroupName { get; set; }
        public List<StudentGroup>? Groups { get; set; }
    }
}
