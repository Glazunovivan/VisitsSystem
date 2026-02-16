using Microsoft.EntityFrameworkCore;
using VisitsApp.Core.Repositories;
using VisitSchool.DataAccessLayer;
using VisitsApp.Core.Models;

namespace VisitSchool.Repositories.SQLite
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationContext _db;

        public ScheduleRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<Schedule> Add(Schedule schedule)
        {
            await _db.Schedules.AddAsync(schedule);
            await _db.SaveChangesAsync();
            return schedule;
        }

        public async Task DeleteSchedule(int scheduleId)
        {
            var deleted = new Schedule { Id = scheduleId };
            _db.Entry(deleted).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Получить расписание по Id
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<Schedule> Get(int scheduleId)
        {
            return await _db.Schedules.AsNoTracking()
                                      .Include(x => x.Days)
                                      .FirstOrDefaultAsync(x=>x.Id == scheduleId);
        }

        /// <summary>
        /// Получить расписание по году и месяцу
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public async Task<Schedule> Get(int year, int month)
        {
            return await _db.Schedules.AsNoTracking()
                                      .Include(x => x.Days)
                                      .FirstOrDefaultAsync(x=>x.Month == month && x.Year == year);
        }

        /// <summary>
        /// Получить все расписания
        /// </summary>
        /// <returns></returns>
        public async Task<List<Schedule>> GetAll()
        {
            return await _db.Schedules.AsNoTracking()
                                      .Include(x=>x.Days)
                                      .ToListAsync();
        }

        /// <summary>
        /// Обновление расписания
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task UpdateSchedule(Schedule schedule)
        {
            // 1. Находим все старые дни этого расписания в базе (БЕЗ AsNoTracking)
            var oldDays = await _db.Days
                                   .Where(d => d.ScheduleId == schedule.Id)
                                   .ToListAsync();

            // 2. Удаляем их
            _db.Days.RemoveRange(oldDays);

            // 3. Обновляем само расписание
            _db.Schedules.Update(schedule);

            // 4. Сохраняем всё одним махом
            await _db.SaveChangesAsync();
        }
    }
}
