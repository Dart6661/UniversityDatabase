using DatabaseModels.UnivModels;

namespace Core.Repositories.Interfaces
{
    public interface ICuratorRepository
    {
        Task<List<Curator>> GetAllAsync();
        Task<Curator> GetCuratorAsync(int id);
        void Create(Curator curator);
        Task UpdateAsync(Curator curator);
        Task DeleteAsync(int id);
    }
}
