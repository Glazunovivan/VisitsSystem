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

        public async Task<Schedule> GetSchedule(int scheduleId)
        {
            try
            {
                return await _repo.Get(scheduleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task DeleteSchedule(int scheduleId)
        {
            try
            {
                await _repo.DeleteSchedule(scheduleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;

            }
        }

        /// <summary>
        /// Поистроить календарь
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<List<DateTime?>> BuildCalendar(int year, int month)
        {
            List<List<DateTime?>> weeks = new List<List<DateTime?>>();

            int daysInMonth = DateTime.DaysInMonth(year, month);
            List<DateTime> allDays = new List<DateTime>();
            for (int day = 1; day <= daysInMonth; day++)
            {
                allDays.Add(new DateTime(year, month, day));
            }

            //построение недели
            var ordered = allDays.OrderBy(x => x).ToList();
            var first = ordered.First();

            int shift = ((int)first.DayOfWeek + 6) % 7; //Пн

            var temp = new List<DateTime?>();

            for (int i = 0; i < shift; i++)
            {
                temp.Add(null);
            }

            foreach (var d in ordered)
            {
                temp.Add(d);
            }

            while (temp.Count % 7 != 0)
            {
                temp.Add(null);
            }

            for (int i = 0; i < temp.Count; i += 7)
            {
                weeks.Add(temp.Skip(i).Take(7).ToList());
            }

            return weeks;
        }
    }
}
