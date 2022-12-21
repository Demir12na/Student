using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Context;
using StudentManagementSystem.Entities;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models.Student;

namespace StudentManagementSystem.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public StudentDbContext _dbContext { get; }

        public StudentRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<StudentInfo> CreateScope()
        {
            return _dbContext.Students;
        }

        public async Task<IList<StudentGetRes>> GetListAsync(StudentGetReq req)
        {
            List<StudentGetRes> res = new List<StudentGetRes>();

            try
            {
                IQueryable<StudentInfo> scope = CreateScope();

                if (req.GroupId != null)
                {
                    scope = scope.Where(s => s.StudentGroupId == req.GroupId);
                }

                if (req.DepartmentId != null)
                {
                    scope = scope.Where(s => s.DepartmentId == req.DepartmentId);
                }

                res = await scope
                    .Select(s => new StudentGetRes
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        EmailAddress = s.EmailAddress,
                        AcademicPerformance = s.AcademicPerformance,
                        GroupName = s.StudentGroups.Name,
                        DepartmentName = s.Department.Name
                    }).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }

        public async Task CreateAsync(StudentCreateReq req)
        {
            StudentInfo student = new StudentInfo();
            try
            {

                if (req.Departments.Id == null)
                {
                    throw new ArgumentException("Department must be selected student creation.");
                }

                student.FirstName = req.FirstName;
                student.LastName = req.LastName;
                student.EmailAddress = req.EmailAddress;
                student.AcademicPerformance = req.AcademicPerformance;
                student.DepartmentId = req.Departments.Id;

                _dbContext.Students.Add(student);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StudentGetUpdateRes> GetUpdateAsync(StudentGetUpdateReq req)
        {
            StudentGetUpdateRes? res = new StudentGetUpdateRes();

            try
            {
                IQueryable<StudentInfo> scope = CreateScope();

                res = await scope.Where(s => s.Id == req.Id)
                    .Select(s => new StudentGetUpdateRes
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        EmailAddress = s.EmailAddress,
                        AcademicPerformance = s.AcademicPerformance,
                        DepartmentName = s.Department.Name
                    }).FirstOrDefaultAsync();

                if (res == null)
                {
                    throw new ArgumentException("Student does not exists.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }

        public async Task UpdateAsync(StudentUpdateReq req)
        {
            try
            {
                IQueryable<StudentInfo> scope = CreateScope();

                StudentInfo? student = await scope.Where(s => s.Id == req.Id).FirstOrDefaultAsync();

                if (student == null)
                {
                    throw new ArgumentException("Student does not exists.");
                }

                student.FirstName = req.FirstName;
                student.LastName = req.LastName;
                student.EmailAddress = req.EmailAddress;
                student.AcademicPerformance = req.AcademicPerformance;

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(StudentDeleteReq req)
        {
            try
            {
                IQueryable<StudentInfo> scope = CreateScope();

                StudentInfo? student = await scope
                    .Where(s => s.Id == req.Id)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    throw new ArgumentException("Student does not exists.");
                }

                _dbContext.Remove(student);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DepartmentGetRes>> GetDepartmentsAsync()
        {
            List<DepartmentGetRes> res = new List<DepartmentGetRes>();

            try
            {
                res = await _dbContext.Departments.Select(d => new DepartmentGetRes
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }

        public async Task AssignGroupAsync(StudentAssignGroupReq req)
        {
            try
            {
                IQueryable<StudentInfo> scope = CreateScope();

                StudentInfo? student = await scope.Where(s => s.Id == req.Id).FirstOrDefaultAsync();

                if (student == null)
                {
                    throw new ArgumentException("Student does not exists.");
                }

                student.StudentGroupId = req.GroupId;

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<StudentGetGroupByStudentIdRes> GetGroupByStudentIdAsync(StudentGetGroupByStudentIdReq req)
        {
            StudentGetGroupByStudentIdRes? res = new StudentGetGroupByStudentIdRes();

            try
            {
                IQueryable<StudentInfo> scope = CreateScope();

                res = await scope.Where(s => s.Id == req.Id)
                    .Select(s => new StudentGetGroupByStudentIdRes
                    {
                        Id = s.Id,
                        DepartmentId = s.DepartmentId,
                        GroupName = s.StudentGroups.Name
                    })
                    .FirstOrDefaultAsync();

                if (res == null)
                {
                    throw new ArgumentException("Student does not exists.");
                }

                res.Groups = await _dbContext.Groups.Where(g => g.DepartmentId == res.DepartmentId).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }
    }
}
