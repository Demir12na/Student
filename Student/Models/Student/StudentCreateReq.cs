using StudentManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Student
{
    public class StudentCreateReq : StudentCreateUpdateBaseReq
    {
        [Required]
        public Department Departments { get; set; }
    }
}
