using Core.ModelsDto;

namespace Core.Facade.Interfaces
{
    public interface IGroupsHandler
    {
        Task CreateGroupAsync(string name, DateTime? creationDate, string curatorName, string curatorEmail);
        Task<GroupDto> GetGroupAsync(int id);
        Task<List<GroupDto>> GetAllGroupsAsync();
        Task<List<StudentDto>> GetStudentsOfGroupAsync(int id);
        Task<int> GetGroupSizeAsync(int id);
        Task UpdateGroupAsync(int id, string? name, DateTime? creationDate);
        Task DeleteGroupAsync(int id);
    }
}
