using GroupManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Context;
using StudentManagementSystem.Entities;
using StudentManagementSystem.Models.Group;

namespace GroupManagementSystem.Repositories
{
    internal class GroupRepository : IGroupRepository
    {
        public StudentDbContext _dbContext { get; }

        public GroupRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<StudentGroup> CreateScope()
        {
            return _dbContext.Groups;
        }

        public async Task<IList<GroupGetRes>> GetListAsync()
        {
            List<GroupGetRes> res = new List<GroupGetRes>();

            try
            {
                IQueryable<StudentGroup> scope = CreateScope();


                res = await scope
                    .Select(g => new GroupGetRes
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }

        public async Task CreateAsync(GroupCreateReq req)
        {
            StudentGroup group = new StudentGroup();
            try
            {

                if (string.IsNullOrWhiteSpace(req.Name))
                {
                    throw new ArgumentException("Group name can not be null or white space.");
                }

                group.Name = req.Name;
                group.DepartmentId = req.Department.Id;

                _dbContext.Groups.Add(group);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GroupGetUpdateRes> GetUpdateAsync(GroupGetUpdateReq req)
        {
            GroupGetUpdateRes? res = new GroupGetUpdateRes();

            try
            {
                IQueryable<StudentGroup> scope = CreateScope();

                res = await scope.Where(g => g.Id == req.Id)
                    .Select(g => new GroupGetUpdateRes
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).FirstOrDefaultAsync();

                if (res == null)
                {
                    throw new ArgumentException("Group does not exists.");
                }

                res.StudentId = req.StudentId;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }

        public async Task UpdateAsync(GroupUpdateReq req)
        {
            try
            {
                IQueryable<StudentGroup> scope = CreateScope();

                StudentGroup? group = await scope.Where(g => g.Id == req.Id).FirstOrDefaultAsync();

                if (group == null)
                {
                    throw new ArgumentException("Group does not exists.");
                }

                group.Name = req.Name;
                group.DepartmentId = req.Department.Id;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(GroupDeleteReq req)
        {
            try
            {
                IQueryable<StudentGroup> scope = CreateScope();

                List<int> students = await _dbContext.Students.Where(s => s.StudentGroupId == req.Id).Select(s => s.Id).ToListAsync();

                if (students != null && students.Count() > 0)
                {
                    throw new InvalidOperationException($"Group can not be deleted because there are {students.Count()} assigned to the group.");
                }

                StudentGroup? group = await scope
                    .Where(s => s.Id == req.Id)
                    .FirstOrDefaultAsync();

                if (group == null)
                {
                    throw new ArgumentException("Group does not exists.");
                }

                _dbContext.Remove(group);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
