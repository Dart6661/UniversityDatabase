using Core.ModelsDto;
using DatabaseModels.UnivModels;

namespace Core.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateStudentAsync(Student student);
        Task<StudentDto> GetStudentAsync(int id);
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<string> GetCuratorNameAsync(int id);
        Task UpdateStudentAsync(StudentDto student);
        Task DeleteStudentAsync(int id);
    }
}
