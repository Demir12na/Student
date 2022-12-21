using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
