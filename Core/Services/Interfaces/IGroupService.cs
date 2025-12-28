using Core.ModelsDto;
using DatabaseModels.UnivModels;

namespace Core.Services.Interfaces
{
    public interface IGroupService
    {
        void CreateGroup(Group group);
        Task<GroupDto> GetGroupAsync(int id);
        Task<List<GroupDto>> GetAllGroupsAsync();
        Task<List<StudentDto>> GetStudentsOfGroupAsync(int id);
        Task UpdateGroupAsync(GroupDto group);
        Task DeleteGroupAsync(int id);
    }
}
