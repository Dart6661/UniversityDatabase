using Core.Facade.Interfaces;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;
using Core.ModelsDto;
using DatabaseModels.UnivModels;

namespace Core.Facade
{
    public class University(ICuratorService cs, IStudentService ss, IGroupService gs, IUnitOfWork uow) : IUniversity
    {
        private readonly ICuratorService _curatorService = cs;
        private readonly IStudentService _studentService = ss;
        private readonly IGroupService _groupService = gs;
        private readonly IUnitOfWork _uow = uow;

        #region Curators

        public async Task CreateCuratorAsync(string name, string email, string groupName, DateTime? groupCreationDate)
        {
            DateTime dateTime = (groupCreationDate ?? DateTime.Now).ToUniversalTime();
            Group group = new() { Name = groupName, CreationDate = dateTime };
            Curator curator = new() { Name = name, Email = email, Group = group };
            group.Curator = curator;
            _groupService.CreateGroup(group);
            _curatorService.CreateCurator(curator);
            await _uow.CommitChangesAsync();
        }

        public async Task<CuratorDto> GetCuratorAsync(int id) => await _curatorService.GetCuratorAsync(id);

        public async Task<List<CuratorDto>> GetAllCuratorsAsync() => await _curatorService.GetAllCuratorsAsync();

        public async Task<int> GetStudentsAverageAgeAsync(int id) => await _curatorService.GetStudentsAverageAgeAsync(id);

        public async Task UpdateCuratorAsync(int id, string? name, string? email)
        {
            if (name == null && email == null) return;
            CuratorDto curator = await _curatorService.GetCuratorAsync(id);
            if (name != null) curator.Name = name;
            if (email != null) curator.Email = email;
            await _curatorService.UpdateCuratorAsync(curator);
            await _uow.CommitChangesAsync();
        }

        public async Task DeleteCuratorAsync(int id)
        {
            await _curatorService.DeleteCuratorAsync(id);
            await _uow.CommitChangesAsync();
        }

        #endregion

        #region Groups

        public async Task CreateGroupAsync(string name, DateTime? creationDate, string curatorName, string curatorEmail)
        {
            DateTime dateTime = (creationDate ?? DateTime.Now).ToUniversalTime();
            Group group = new() { Name = name, CreationDate = dateTime };
            Curator curator = new() { Name = curatorName, Email = curatorEmail, Group = group };
            group.Curator = curator;
            _groupService.CreateGroup(group);
            await _uow.CommitChangesAsync();
        }

        public async Task<GroupDto> GetGroupAsync(int id) => await _groupService.GetGroupAsync(id);

        public async Task<List<GroupDto>> GetAllGroupsAsync() => await _groupService.GetAllGroupsAsync();

        public async Task<List<StudentDto>> GetStudentsOfGroupAsync(int id) => await _groupService.GetStudentsOfGroupAsync(id);

        public async Task<int> GetGroupSizeAsync(int id) => (await _groupService.GetStudentsOfGroupAsync(id)).Count;

        public async Task UpdateGroupAsync(int id, string? name, DateTime? creationDate)
        {
            if (name == null && creationDate == null) return;
            GroupDto group = await _groupService.GetGroupAsync(id);
            if (name != null) group.Name = name;
            if (creationDate != null) group.CreationDate = (DateTime)creationDate;
            await _groupService.UpdateGroupAsync(group);
            await _uow.CommitChangesAsync();
        }

        public async Task DeleteGroupAsync(int id)
        {
            await _groupService.DeleteGroupAsync(id);
            await _uow.CommitChangesAsync();
        }

        #endregion

        #region Students

        public async Task CreateStudentAsync(string name, int age, int groupId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(age);
            _ = await _groupService.GetGroupAsync(groupId);
            Student student = new() { Name = name, Age = age, GroupId = groupId };
            await _studentService.CreateStudentAsync(student);
            await _uow.CommitChangesAsync();
        }

        public async Task<StudentDto> GetStudentAsync(int id) => await _studentService.GetStudentAsync(id);

        public async Task<List<StudentDto>> GetAllStudentsAsync() => await _studentService.GetAllStudentsAsync();

        public async Task<string> GetCuratorNameAsync(int id) => await _studentService.GetCuratorNameAsync(id);

        public async Task UpdateStudentAsync(int id, string? name, int? age, int? groupId)
        {
            if (name == null && age == null && groupId == null) return;
            StudentDto student = await _studentService.GetStudentAsync(id);
            if (name != null) student.Name = name;
            if (age != null)
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)age);
                student.Age = (int)age;
            }
            if (groupId != null)
            {
                GroupDto currentGroup = await _groupService.GetGroupAsync(student.GroupId);
                GroupDto newGroup = await _groupService.GetGroupAsync((int)groupId);
                currentGroup.Students.Remove(student);
                student.GroupId = (int)groupId;
                newGroup.Students.Add(student);
                await _groupService.UpdateGroupAsync(currentGroup);
                await _groupService.UpdateGroupAsync(newGroup);
            }
            await _studentService.UpdateStudentAsync(student);
            await _uow.CommitChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            await _uow.CommitChangesAsync();
        }

        #endregion

    }
}
