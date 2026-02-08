using VisitSchool.Models;

namespace VisitSchool.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> AddAsync(Student student);

        Task<Student> GetByIdAsync(int id);

        Task<List<Student>> GetAllAsync();

        Task UpdateAsync(Student student);

        Task DeleteAsync(int id);

        Task<List<DiscountCategory>> GetAllDiscountCategories();
    }
}
