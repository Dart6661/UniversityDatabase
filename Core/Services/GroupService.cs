using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Core.ModelsDto;
using Core.Serializers;
using DatabaseModels.UnivModels;

namespace Core.Services
{
    public class GroupService(IGroupRepository groupRepos) : IGroupService
    {
        private readonly IGroupRepository _groupRepos = groupRepos;

        public void CreateGroup(Group group)
        {
            ArgumentNullException.ThrowIfNull(group);
            _groupRepos.Create(group);
        }

        public async Task<GroupDto> GetGroupAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            return GroupSerializer.Serialize(await _groupRepos.GetGroupAsync(id));
        }

        public async Task<List<GroupDto>> GetAllGroupsAsync() => [.. (await _groupRepos.GetAllAsync()).Select(group => GroupSerializer.Serialize(group))];

        public async Task<List<StudentDto>> GetStudentsOfGroupAsync(int id)
        {
            GroupDto group = await GetGroupAsync(id);
            return [.. group.Students];
        }

        public async Task UpdateGroupAsync(GroupDto group)
        {
            ArgumentNullException.ThrowIfNull(group);
            await _groupRepos.UpdateAsync(GroupSerializer.Deserialize(group));
        }

        public async Task DeleteGroupAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            await _groupRepos.DeleteAsync(id);
        }
    }
}
