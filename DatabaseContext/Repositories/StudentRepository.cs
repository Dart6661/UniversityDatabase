using Core.Repositories.Interfaces;
using DatabaseContext.Context;
using DatabaseModels.UnivModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.Repositories
{
    public class StudentRepository(UnivContext context) : IStudentRepository
    {
        private readonly UnivContext _context = context;

        public async Task<List<Student>> GetAllAsync() => await QueryAll().ToListAsync();
        
        public async Task<Student> GetStudentAsync(int id) => await QueryAll()
            .FirstOrDefaultAsync(s => s.Id == id) ?? throw new KeyNotFoundException($"student {id} not found");

        public void Create(Student student) => _context.Students.Add(student);

        public async Task UpdateAsync(Student student)
        {
            Student currentStudent = await GetStudentAsync(student.Id);
            currentStudent.Name = student.Name;
            currentStudent.Age = student.Age;
        }
        
        public async Task DeleteAsync(int id) => _context.Students.Remove(await GetStudentAsync(id));

        private IQueryable<Student> QueryAll() => _context.Students.Include(g => g.Group);
    }
}
