using StudentManagementSystem.Entities;

namespace StudentManagementSystem.Models.Group
{
    public class GroupCreateUpdateBaseReq
    {
        public string Name { get; set; }
        public Department Department { get; set; }
    }
}
