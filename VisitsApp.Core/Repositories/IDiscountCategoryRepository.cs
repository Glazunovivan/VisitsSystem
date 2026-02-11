using VisitsApp.Core.Models;

namespace VisitsApp.Core.Repositories
{
    public interface IDiscountCategoryRepository
    {
        Task Add(string name, double discount);

        Task Delete(int id);

        Task<List<DiscountCategory>> GetAll();
    }
}
