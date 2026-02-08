using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitSchool.Models;
using VisitSchool.Repositories;

namespace VisitSchool.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _repo;

        public ScheduleService(IScheduleRepository repo)
        {
            _repo = repo;    
        }

        /// <summary>
        /// Добавление расписания
        /// </summary>
        /// <returns></returns>
        public async Task AddSchedule(Schedule schedule)
        {
            await _repo.Add(schedule);
        }

        /// <summary>
        /// Обновить расписание
        /// </summary>
        /// <returns></returns>
        public async Task UpdateShedule(Schedule schedule)
        {
            await _repo.UpdateSchedule(schedule);   
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            return await _repo.GetAll();
        }

        public Task<Schedule> GetSchedule(int scheduleId)
        {
            return _repo.Get(scheduleId);
        }
    }
}
