using Microsoft.EntityFrameworkCore;
using System;
using VisitSchool.DataAccessLayer;
using VisitSchool.Dtos;
using VisitSchool.Models;

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
            var day = _db.Schedules.First(x => x.Id == visit.ScheduleId).Days.FirstOrDefault(x=>x.Day == visit.Day);
            var newVisit = new Visit
            {
                Day = visit.Day,
                ScheduleId = visit.ScheduleId,
                Status = visit.Status,
                StudentId = visit.StudentId,
                ScheduleDay = day
            };
            try
            {
                await _db.Visits.AddAsync(newVisit);
                await _db.SaveChangesAsync();   
            }
            catch (Exception ex)
            {

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

        public Task<List<Visit>> GetVisitByStudentId()
        {
            throw new NotImplementedException();
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
            _db.SaveChanges();
        }
    }
}
