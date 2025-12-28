using Core.Repositories.Interfaces;
using DatabaseContext.Context;
using DatabaseModels.UnivModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.Repositories
{
    public class CuratorRepository(UnivContext context) : ICuratorRepository
    {
        private readonly UnivContext _context = context;

        public async Task<List<Curator>> GetAllAsync() => await QueryAll().ToListAsync();

        public async Task<Curator> GetCuratorAsync(int id) => await QueryAll()
            .FirstOrDefaultAsync(c => c.Id == id) ?? throw new KeyNotFoundException($"curator {id} not found");

        public void Create(Curator curator) => _context.Curators.Add(curator);

        public async Task UpdateAsync(Curator curator)
        {
            Curator currentCurator = await GetCuratorAsync(curator.Id);
            currentCurator.Name = curator.Name;
            currentCurator.Email = curator.Email;
        }

        public async Task DeleteAsync(int id) => _context.Curators.Remove(await GetCuratorAsync(id));

        private IQueryable<Curator> QueryAll() => _context.Curators.Include(g => g.Group);
    }
}
