using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Entities
{
    public class StudentInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(300)]
        public string LastName { get; set; }

        [Required]
        [StringLength(300)]
        public string EmailAddress { get; set; }

        [Range(0,5)]
        public decimal? AcademicPerformance { get; set; }

        public StudentGroup StudentGroups { get; set; }
        public int? StudentGroupId { get; set; }

        public Department Department { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
