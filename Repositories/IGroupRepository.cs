using VisitSchool.Models;

namespace VisitSchool.Repositories
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAllGroupsAsync();

        Task Add(string name);

        Task Delete(int id);

        Task Update(int id, string name);
    }
}
