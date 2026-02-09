using VisitSchool.Models;
using VisitSchool.Repositories;

namespace VisitSchool.Services
{
    public class GroupService
    {
        private readonly IGroupRepository _repo;

        public GroupService(IGroupRepository repo)
        {
            _repo = repo;   
        }

        /// <summary>
        /// Получить все группы
        /// </summary>
        /// <returns></returns>
        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _repo.GetAllGroupsAsync();
        }

        /// <summary>
        /// Добавить группу
        /// </summary>
        /// <returns></returns>
        public Task AddGroup(string name)
        {
            return _repo.Add(name);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
