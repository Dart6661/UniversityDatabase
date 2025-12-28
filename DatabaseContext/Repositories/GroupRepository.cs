using Core.Repositories.Interfaces;
using DatabaseContext.Context;
using DatabaseModels.UnivModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.Repositories
{
    public class GroupRepository(UnivContext context) : IGroupRepository
    {
        private readonly UnivContext _context = context;

        public async Task<List<Group>> GetAllAsync() => await QueryAll().ToListAsync();

        public async Task<Group> GetGroupAsync(int id) => await QueryAll()
            .FirstOrDefaultAsync(g => g.Id == id) ?? throw new KeyNotFoundException($"group {id} not found");

        public void Create(Group group) => _context.Groups.Add(group);

        public async Task UpdateAsync(Group group)
        {
            Group currentGroup = await GetGroupAsync(group.Id);
            currentGroup.Name = group.Name;
            currentGroup.CreationDate = group.CreationDate;
            currentGroup.Students.Clear();
            foreach (var student in group.Students)
                currentGroup.Students.Add(await _context.Students.FindAsync(student.Id) ?? throw new InvalidOperationException($"student {student.Id} not found"));
            ArgumentNullException.ThrowIfNull(group.Curator);
            if (currentGroup.Curator == null) currentGroup.Curator = group.Curator;
            else
            {
                currentGroup.Curator.Name = group.Curator.Name;
                currentGroup.Curator.Email = group.Curator.Email;
            }
        }

        public async Task DeleteAsync(int id) => _context.Groups.Remove(await GetGroupAsync(id));

        private IQueryable<Group> QueryAll()
        {
            return _context.Groups
                    .Include(g => g.Curator)
                    .Include(g => g.Students);
        }
    }
}
