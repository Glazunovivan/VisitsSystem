using Microsoft.Extensions.Logging;
using VisitsApp.Core.Models;
using VisitsApp.Core.Repositories;

namespace VisitsApp.Core.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _repo;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IScheduleRepository repo, ILogger<ScheduleService> logger)
        {
            _repo = repo;  
            _logger = logger;
        }

        /// <summary>
        /// Добавление расписания
        /// </summary>
        /// <returns></returns>
        public async Task AddSchedule(Schedule schedule)
        {
            try
            {
                await _repo.Add(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Обновить расписание
        /// </summary>
        /// <returns></returns>
        public async Task UpdateShedule(Schedule schedule)
        {
            try
            {
                await _repo.UpdateSchedule(schedule);   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            try
            {
               return await _repo.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public Task<Schedule> GetSchedule(int scheduleId)
        {
            try
            {
                return _repo.Get(scheduleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
