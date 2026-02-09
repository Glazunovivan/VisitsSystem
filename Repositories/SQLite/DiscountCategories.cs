using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitSchool.DataAccessLayer;
using VisitSchool.Models;

namespace VisitSchool.Repositories.SQLite
{
    public class DiscountCategories : IDiscountCategoryRepository
    {
        private readonly ApplicationContext _db;

        public DiscountCategories(ApplicationContext db)
        {
            _db = db;   
        }

        public async Task Add(string name, double discount)
        {
            var discountCategory = new DiscountCategory
            {
                DscountPercent = discount,
                Name = name,
            };

            await _db.StudentCategories.AddAsync(discountCategory);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var dc = await _db.StudentCategories.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);

            if (dc != null)
            {
                _db.Entry(dc).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
            }   
        }

        public async Task<List<DiscountCategory>> GetAll()
        {
            return await _db.StudentCategories.AsNoTracking().ToListAsync();
        }
    }
}
