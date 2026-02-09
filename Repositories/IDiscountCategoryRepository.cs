using VisitSchool.Models;

namespace VisitSchool.Repositories
{
    public interface IDiscountCategoryRepository
    {
        Task Add(string name, double discount);

        Task Delete(int id);

        Task<List<DiscountCategory>> GetAll();
    }
}
