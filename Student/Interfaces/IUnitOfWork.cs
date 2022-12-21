using GroupManagementSystem.Interfaces;

namespace StudentManagementSystem.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository Students { get; }
        public IGroupRepository Groups { get; }
    }
}
