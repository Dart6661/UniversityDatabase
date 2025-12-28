using Core.ModelsDto;

namespace Core.Facade.Interfaces
{
    public interface IStudentsHandler
    {
        Task CreateStudentAsync(string name, int age, int groupId);
        Task<StudentDto> GetStudentAsync(int id);
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<string> GetCuratorNameAsync(int id);
        Task UpdateStudentAsync(int id, string? name, int? age, int? groupId);
        Task DeleteStudentAsync(int id);
    }
}
