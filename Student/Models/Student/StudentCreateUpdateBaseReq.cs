using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Student
{
    public class StudentCreateUpdateBaseReq
    {
        [Required]
        [StringLength(300)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(300)]
        public string LastName { get; set; }

        [Required]
        [StringLength(300)]
        public string EmailAddress { get; set; }

        public decimal? AcademicPerformance { get; set; }
    }
}
