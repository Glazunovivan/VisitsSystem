using Microsoft.EntityFrameworkCore;
using VisitSchool.DataAccessLayer;
using VisitSchool.Models;

namespace VisitSchool.Repositories.SQLite
{
    public class StudentsRepository : IStudentRepository
    {
        private readonly ApplicationContext _dpContext;
        public StudentsRepository(ApplicationContext dbContext)
        {
            _dpContext = dbContext;
        }

        public async Task<Student> AddAsync(Student student)
        {
            student.CreatedAt = DateTime.Now;   

            await _dpContext.Students.AddAsync(student);
            await _dpContext.SaveChangesAsync();
            
            return student;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteStudent = new Student { Id = id };
            _dpContext.Entry(deleteStudent).State = EntityState.Deleted;
            await _dpContext.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dpContext.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _dpContext.Students.FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new NullReferenceException($"Ученик с Id {id} не найден в системе");
        }

        public async Task UpdateAsync(Student student)
        {
            _dpContext.Entry(student).State = EntityState.Modified;
            await _dpContext.SaveChangesAsync();
        }

        public async Task<List<DiscountCategory>> GetAllDiscountCategories()
        {
            return await _dpContext.StudentCategories.AsNoTracking().ToListAsync();
        }
    }
}
