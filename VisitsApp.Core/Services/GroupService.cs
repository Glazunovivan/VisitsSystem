using Microsoft.Extensions.Logging;
using VisitsApp.Core.Models;
using VisitsApp.Core.Repositories;

namespace VisitsApp.Core.Services
{
    public class GroupService
    {
        private readonly IGroupRepository _repo;
        private readonly ILogger<GroupService> _logger;

        public GroupService(IGroupRepository repo, ILogger<GroupService> logger)
        {
            _repo = repo;   
            _logger = logger;
        }

        /// <summary>
        /// Получить все группы
        /// </summary>
        /// <returns></returns>
        public async Task<List<Group>> GetAllGroupsAsync()
        {
            try
            {
                return await _repo.GetAllGroupsAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Добавить группу
        /// </summary>
        /// <returns></returns>
        public Task AddGroup(string name)
        {
            try
            {
                return _repo.Add(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;   
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _repo.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;   
            }
        }
    }
}
