using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Entities
{
    public class StudentGroup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public Department Department { get; set; }
        [Required]
        public int DepartmentId { get; set; }

    }
}
