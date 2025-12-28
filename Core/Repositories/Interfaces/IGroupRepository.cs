using DatabaseModels.UnivModels;

namespace Core.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAllAsync();
        Task<Group> GetGroupAsync(int id);
        void Create(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(int id);
    }
}
