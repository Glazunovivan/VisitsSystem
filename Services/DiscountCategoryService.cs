using VisitSchool.Models;
using VisitSchool.Repositories;

namespace VisitSchool.Services
{
    public class DiscountCategoryService
    {
        private readonly IDiscountCategoryRepository _repository;
        public DiscountCategoryService(IDiscountCategoryRepository repo)
        {
            _repository = repo; 
        }

        public async Task Add(string name, double percent)
        {
            try
            {
                await _repository.Add(name, percent);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<DiscountCategory>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
