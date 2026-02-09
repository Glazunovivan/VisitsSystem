using System.Collections.Concurrent;
using VisitSchool.Dtos;
using VisitSchool.Models;

namespace VisitSchool.Repositories
{
    public class InMemoryVisitsRepository : IVisitsRepository
    {
        private readonly ConcurrentDictionary<int, Visit> _store = new();
        private int _idCounter = 0;

        public InMemoryVisitsRepository()
        {
            _store.Clear();
        }

        public Task AddVisit(AddStudentVisitDto visitDto)
        {
            _idCounter++;

            var existsVisit = _store.Values.FirstOrDefault(x => x.StudentId == visitDto.StudentId && x.Day == visitDto.Day);
            if (existsVisit != null)
            {
                //обновляем
                existsVisit.StudentId = visitDto.StudentId;
                existsVisit.ScheduleId = visitDto.ScheduleId;
                existsVisit.ScheduleDay = new ScheduleDay
                {
                    Day = visitDto.Day,
                    ScheduleId = visitDto.ScheduleId
                };
                existsVisit.Day = visitDto.Day;
                existsVisit.Status = visitDto.Status;
            }
            else
            {
                //добавляем новый
                _store.TryAdd(_idCounter, new Visit
                {
                    StudentId = visitDto.StudentId,
                    ScheduleId = visitDto.ScheduleId,
                    ScheduleDay = new ScheduleDay
                    {
                        Day = visitDto.Day,
                        ScheduleId = visitDto.ScheduleId
                    },
                    Day = visitDto.Day,
                    Status = visitDto.Status
                });
            }

            return Task.CompletedTask;
        }

        public Task<List<Visit>> GetVisitsByDate(DateTime dateTime)
        {
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day; 

            var list = _store.Values.Where(x=>x.Day == day && x.Schedule.Year == year && x.Schedule.Month == month).ToList();

            return Task.FromResult(list);
        }

        public Task<List<Visit>> GetAllVisitsAsync()
        {
            var list = _store.Values.OrderByDescending(s => s)
                                    .ToList();
            return Task.FromResult(list);
        }

        public Task<List<Visit>> GetAllVisitsAsync(DateTime startDate, DateTime endDate)
        {
            var list = _store.Values.OrderByDescending(s => s)
                                    .ToList();
            return Task.FromResult(list);
        }

        public Task<List<Visit>> GetVisitByStudentId(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVisitStudent(DateTime currentDateTime, DateTime newDateTime, int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Visit>> GetVisitsByScheduleId(int scheduleId)
        {
            var result = _store.Values.Where(x=>x.ScheduleId == scheduleId).ToList();

            return Task.FromResult(result);
        }

        public Task IncludeSchedule(int scheduleId, Schedule schedule)
        {
            foreach (var v in _store.Values.Where(x=>x.ScheduleId == scheduleId))
            {
                v.Schedule = schedule;
            }

            return Task.CompletedTask;
        }

        public Task<Visit> GetVisitByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusVisitStudent(int scheduleId, int day, int studentId, int status)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVisit(int scheduleId, int day)
        {
            throw new NotImplementedException();
        }

        public Task<Visit> GetVisitByStudentDate(int scheduleId, int day, int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
