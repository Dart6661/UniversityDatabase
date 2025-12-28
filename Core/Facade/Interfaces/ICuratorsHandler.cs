using Core.ModelsDto;

namespace Core.Facade.Interfaces
{
    public interface ICuratorsHandler
    {
        Task CreateCuratorAsync(string name, string email, string groupName, DateTime? groupCreationDate);
        Task<CuratorDto> GetCuratorAsync(int id);
        Task<List<CuratorDto>> GetAllCuratorsAsync();
        Task<int> GetStudentsAverageAgeAsync(int id);
        Task UpdateCuratorAsync(int id, string? name, string? email);
        Task DeleteCuratorAsync(int id);
    }
}
