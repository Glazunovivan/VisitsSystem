using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VisitSchool.DataAccessLayer;
using VisitsApp.Core.Dtos;
using VisitsApp.Core.Models;
using VisitsApp.Core.Repositories;

namespace VisitSchool.Repositories.SQLite
{
    public class VisitsRepository : IVisitsRepository
    {
        private readonly ApplicationContext _db;

        public VisitsRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task AddVisit(AddStudentVisitDto visit)
        {
            var day = _db.Schedules.Include(x=>x.Days)
                                   .First(x => x.Id == visit.ScheduleId)
                                   .Days
                                   .FirstOrDefault(x => x.Day == visit.Day);
            var newVisit = new Visit
            {
                Day = visit.Day,
                ScheduleId = visit.ScheduleId,
                Status = visit.Status,
                StudentId = visit.StudentId,
                ScheduleDay = day
            };

            await _db.Visits.AddAsync(newVisit);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAllVisits(int scheduleId)
        {
            var visitsToDelete = await _db.Visits.AsNoTracking().Where(x=>x.ScheduleId == scheduleId).ToListAsync();

            _db.Visits.RemoveRange(visitsToDelete);

            await _db.SaveChangesAsync();   
        }

        public async Task DeleteVisit(int scheduleId, int day)
        {
            var existVisit = await _db.Visits.AsNoTracking().FirstOrDefaultAsync(x=>x.ScheduleId == scheduleId && x.Day == day);
            if (existVisit != null)
            {
                _db.Visits.Entry(existVisit).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Visit>> GetAllVisitsAsync()
        {
            return await _db.Visits.AsNoTracking()
                                   .Include(x=>x.Schedule)
                                   .Include(x=>x.ScheduleDay)
                                   .ToListAsync();    
        }

        public async Task<List<Visit>> GetAllVisitsAsync(DateTime startDate, DateTime endDate)
        {
            return await _db.Visits.AsNoTracking()
                                   .Include(x => x.Schedule)
                                   .Include(x => x.ScheduleDay)
                                   .Where(x=>(x.Day >= startDate.Day && x.Schedule.Year >= startDate.Year && x.Schedule.Month >= startDate.Month) &&
                                             (x.Day <= endDate.Day && x.Schedule.Year <= endDate.Year && x.Schedule.Month <= endDate.Month))
                                   .ToListAsync();

        }

        public async Task<Visit> GetVisitByDate(DateTime dateTime)
        {
            return await _db.Visits.AsNoTracking()
                                   .Include(x => x.Schedule)
                                   .Include(x => x.ScheduleDay)
                                   .FirstOrDefaultAsync(x => x.Day == dateTime.Day && x.Schedule.Month == dateTime.Month && x.Schedule.Year == dateTime.Year);
                                   
        }


        public async Task<Visit> GetVisitByStudentDate(int scheduleId, int day, int studentId)
        {
            return await _db.Visits.AsNoTracking()
                                   .FirstOrDefaultAsync(x=>x.ScheduleId == scheduleId && x.Day == day && x.StudentId == studentId);
        }

        public async Task<List<Visit>> GetVisitByStudentId(int id)
        {
            return await _db.Visits.Include(x=>x.Day)
                                   .Include(x=>x.Student)
                                   .Include(x=>x.Schedule)
                                   .Where(x=>x.StudentId == id)
                                   .ToListAsync();
        }

        public async Task<List<Visit>> GetVisitsByDate(DateTime dateTime)
        {
            return await _db.Visits.AsNoTracking()
                                   .Include(x => x.Schedule)
                                   .Include(x => x.ScheduleDay)
                                   .Where(x => x.Day == dateTime.Day && x.Schedule.Month == dateTime.Month && x.Schedule.Year == dateTime.Year)
                                   .ToListAsync();
        }

        public async Task<List<Visit>> GetVisitsByScheduleId(int scheduleId)
        {
            return await _db.Visits.AsNoTracking()
                                   .Include(x => x.Schedule)
                                   .Include(x => x.ScheduleDay)
                                   .Where(x => x.ScheduleId == scheduleId)
                                   .ToListAsync();
        }

        public Task IncludeSchedule(int scheduleId, Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStatusVisitStudent(int scheduleId, int day, int studentId, int status)
        {
            var visit = await _db.Visits.AsNoTracking().FirstOrDefaultAsync(x => x.ScheduleId == scheduleId && x.Day == day && x.StudentId == studentId);

            visit.Status = status;
            _db.Entry(visit).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateVisitStudent(DateTime currentDateTime, DateTime newDateTime, int studentId)
        {

            var existsVisit = await _db.Visits.AsNoTracking()
                                              .Include(x=>x.Schedule)
                                              .FirstOrDefaultAsync(x=>x.StudentId == studentId && (x.Day == currentDateTime.Day && x.Schedule.Month == currentDateTime.Month && x.Schedule.Year == currentDateTime.Year));

            if (existsVisit == null)
            {
                return;
            }
            existsVisit.Day = newDateTime.Day;
            existsVisit.Schedule.Year = newDateTime.Year;
            existsVisit.Schedule.Month = newDateTime.Month;

            _db.Entry(existsVisit).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
