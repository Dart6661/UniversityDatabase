using DatabaseModels.UnivModels;

namespace Core.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetStudentAsync(int id);
        void Create(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
