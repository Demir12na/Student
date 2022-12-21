using StudentManagementSystem.Models.Group;

namespace GroupManagementSystem.Interfaces
{
    public interface IGroupRepository
    {
        Task<IList<GroupGetRes>> GetListAsync();
        Task CreateAsync(GroupCreateReq req);
        Task<GroupGetUpdateRes> GetUpdateAsync(GroupGetUpdateReq req);
        Task UpdateAsync(GroupUpdateReq req);
        Task DeleteAsync(GroupDeleteReq req);
    }
}
