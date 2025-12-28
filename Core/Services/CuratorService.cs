using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Core.ModelsDto;
using Core.Serializers;
using DatabaseModels.UnivModels;

namespace Core.Services
{
    public class CuratorService(ICuratorRepository cr, IGroupRepository gr) : ICuratorService
    {
        private readonly ICuratorRepository _curatorRepos = cr;
        private readonly IGroupRepository _groupRepos = gr;

        public void CreateCurator(Curator curator)
        {
            ArgumentNullException.ThrowIfNull(curator);
            _curatorRepos.Create(curator);
        }

        public async Task<CuratorDto> GetCuratorAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            return CuratorSerializer.Serialize(await _curatorRepos.GetCuratorAsync(id));
        }

        public async Task<List<CuratorDto>> GetAllCuratorsAsync() => [.. (await _curatorRepos.GetAllAsync()).Select(curator => CuratorSerializer.Serialize(curator))];

        public async Task<int> GetStudentsAverageAgeAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            Curator curator = await _curatorRepos.GetCuratorAsync(id);
            Group group = await _groupRepos.GetGroupAsync(curator.GroupId);
            List<Student> students = [.. group.Students];
            if (students.Count == 0) return 0;
            int sum = 0, count = 0;
            foreach (var student in students)
            {
                sum += student.Age;
                count++;
            }
            return sum / count;
        }

        public async Task UpdateCuratorAsync(CuratorDto curator)
        {
            ArgumentNullException.ThrowIfNull(curator);
            await _curatorRepos.UpdateAsync(CuratorSerializer.Deserialize(curator));
        }

        public async Task DeleteCuratorAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            Curator curator = await _curatorRepos.GetCuratorAsync(id);
            Group group = await _groupRepos.GetGroupAsync(curator.GroupId);
            await _groupRepos.DeleteAsync(group.Id);
        }
    }
}
