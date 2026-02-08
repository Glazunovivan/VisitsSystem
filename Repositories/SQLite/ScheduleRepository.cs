using Microsoft.EntityFrameworkCore;
using VisitSchool.DataAccessLayer;
using VisitSchool.Models;

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

        /// <summary>
        /// Получить расписание по Id
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<Schedule> Get(int scheduleId)
        {
            return await _db.Schedules.Include(x => x.Days)
                                      .AsNoTracking()
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
            return await _db.Schedules.Include(x => x.Days)
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(x=>x.Month == month && x.Year == year);
        }

        /// <summary>
        /// Получить все расписания
        /// </summary>
        /// <returns></returns>
        public async Task<List<Schedule>> GetAll()
        {
            return await _db.Schedules.Include(x=>x.Days)
                                      .AsNoTracking()
                                      .ToListAsync();
        }

        /// <summary>
        /// Обновление расписания
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task UpdateSchedule(Schedule schedule)
        {
            _db.Entry(schedule).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
