using Core.ModelsDto;
using DatabaseModels.UnivModels;

namespace Core.Services.Interfaces
{
    public interface ICuratorService
    {
        void CreateCurator(Curator curator);
        Task<CuratorDto> GetCuratorAsync(int id);
        Task<List<CuratorDto>> GetAllCuratorsAsync();
        Task<int> GetStudentsAverageAgeAsync(int id);
        Task UpdateCuratorAsync(CuratorDto curator);
        Task DeleteCuratorAsync(int id);
    }
}
