using Microsoft.EntityFrameworkCore;
using VisitSchool.DataAccessLayer;
using VisitsApp.Core.Models;
using VisitsApp.Core.Repositories;

namespace VisitSchool.Repositories.SQLite
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationContext _db;
        public GroupRepository(ApplicationContext db)
        {
            _db = db;   
        }

        public async Task Add(string name)
        {
            var newGroup = new Group
            {
                Name = name
            };

            await _db.Groups.AddAsync(newGroup);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _db.Groups.Include(x=>x.Students)
                                   .AsNoTracking()
                                   .ToListAsync();
        }

        public Task Update(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
