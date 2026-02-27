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
            var schedule = await _db.Schedules
                                  .Include(s => s.Days) // Загружаем связанные дни, чтобы EF их тоже удалил
                                  .FirstOrDefaultAsync(s => s.Id == scheduleId);

            if (schedule != null)
            {
                _db.Schedules.Remove(schedule);
                await _db.SaveChangesAsync();
            }
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
                                      .Include(x=> x.Groups)
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
                                      .Include(x => x.Groups)
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
                                      .Include(x=>x.Groups)
                                      .ToListAsync();
        }

        /// <summary>
        /// Обновление расписания
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task UpdateSchedule(Schedule schedule)
        {
            //удаляем старые дни
            var oldDays = _db.Days.Where(d => d.ScheduleId == schedule.Id);
            _db.Days.RemoveRange(oldDays);

            // Ищем в трекере объект Schedule с таким же Id
            var trackedEntity = _db.Schedules.Local.FirstOrDefault(x => x.Id == schedule.Id);

            if (trackedEntity != null)
            {
                // Если нашли — отсоединяем его, чтобы прикрепить новый
                _db.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Теперь можно безопасно вызвать Update
            _db.Schedules.Update(schedule);
            await _db.SaveChangesAsync();
        }
    }
}
