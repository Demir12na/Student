using StudentManagementSystem.Models.Student;

namespace StudentManagementSystem.Interfaces
{
    public interface IStudentRepository
    {
        Task<IList<StudentGetRes>> GetListAsync(StudentGetReq req);
        Task CreateAsync(StudentCreateReq req);
        Task<StudentGetUpdateRes> GetUpdateAsync(StudentGetUpdateReq req);
        Task UpdateAsync(StudentUpdateReq req);
        Task DeleteAsync(StudentDeleteReq req);
        Task<List<DepartmentGetRes>> GetDepartmentsAsync();
        Task AssignGroupAsync(StudentAssignGroupReq req);
        Task<StudentGetGroupByStudentIdRes> GetGroupByStudentIdAsync(StudentGetGroupByStudentIdReq req);
    }
}
