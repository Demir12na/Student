using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Student
{
    public class StudentGetGroupByStudentIdReq
    {
        [Required]
        public int? Id { get; set; }
    }
}
