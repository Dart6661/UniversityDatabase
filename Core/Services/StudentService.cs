using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Core.ModelsDto;
using Core.Serializers;
using DatabaseModels.UnivModels;

namespace Core.Services
{
    public class StudentService(IStudentRepository sr, IGroupRepository gr) : IStudentService
    {
        private readonly IStudentRepository _studentRepos = sr;
        private readonly IGroupRepository _groupRepos = gr;

        public async Task CreateStudentAsync(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            _ = await _groupRepos.GetGroupAsync(student.GroupId);
            _studentRepos.Create(student);
        }

        public async Task<StudentDto> GetStudentAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            return StudentSerializer.Serialize(await _studentRepos.GetStudentAsync(id));
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync() => [.. (await _studentRepos.GetAllAsync()).Select(student => StudentSerializer.Serialize(student))];

        public async Task<string> GetCuratorNameAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            Student student = await _studentRepos.GetStudentAsync(id);
            Group group = await _groupRepos.GetGroupAsync(student.GroupId);
            Curator curator = group.Curator ?? throw new InvalidOperationException("group must have a curator");
            return curator.Name;
        }

        public async Task UpdateStudentAsync(StudentDto student)
        {
            ArgumentNullException.ThrowIfNull(student);
            await _studentRepos.UpdateAsync(StudentSerializer.Deserialize(student));
        }

        public async Task DeleteStudentAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            await _studentRepos.DeleteAsync(id);
        }
    }
}
