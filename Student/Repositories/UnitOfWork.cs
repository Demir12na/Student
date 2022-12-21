using GroupManagementSystem.Interfaces;
using StudentManagementSystem.Interfaces;

namespace StudentManagementSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IStudentRepository Students { get; }
        public IGroupRepository Groups { get; }

        public UnitOfWork(IStudentRepository students, IGroupRepository groups)
        {
            Students = students; 
            Groups = groups;
        }
    }
}
