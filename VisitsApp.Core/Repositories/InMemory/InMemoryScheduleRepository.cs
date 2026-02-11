using VisitsApp.Core.Models;

namespace VisitsApp.Core.Repositories
{
    public class InMemoryScheduleRepository : IScheduleRepository
    {
        public List<Schedule> Schedules { get; set; }

        public InMemoryScheduleRepository()
        {
            Schedules = new List<Schedule>()
            {
                new Schedule
                {
                    Id = 1,
                    Year = 2026,
                    Month = 2,
                    ScheduleName = "Февраль 2026",
                    CostSubscriptions = 8800,
                    Days = new List<ScheduleDay>
                    {
                         new ScheduleDay { ScheduleId = 1, Day = 1 },
                         new ScheduleDay { ScheduleId = 1, Day = 2 },
                         new ScheduleDay { ScheduleId = 1, Day = 3 },
                         new ScheduleDay { ScheduleId = 1, Day = 7 },
                         new ScheduleDay { ScheduleId = 1, Day = 8 },
                         new ScheduleDay { ScheduleId = 1, Day = 9 },
                         new ScheduleDay { ScheduleId = 1, Day = 12 },
                         new ScheduleDay { ScheduleId = 1, Day = 18 },
                    }
                },
                new Schedule
                {
                    Id = 2,
                    Year = 2026,
                    Month = 3,
                    ScheduleName = "Март 2026",
                    CostSubscriptions = 8800,
                    Days = new List<ScheduleDay>
                    {
                        new ScheduleDay { ScheduleId = 2, Day = 5 },
                        new ScheduleDay { ScheduleId = 2, Day = 8 },
                        new ScheduleDay { ScheduleId = 2, Day = 10 },
                        new ScheduleDay { ScheduleId = 2, Day = 12 },
                        new ScheduleDay { ScheduleId = 2, Day = 18 },
                    }
                }
            };

        }

        public async Task<List<Schedule>> GetAll()
        {
            return await Task.FromResult(Schedules);
        }

        public async Task<Schedule> Add(Schedule schedule)
        {
            Schedules.Add(schedule);

            return await Task.FromResult(schedule);
        }

        public Task<Schedule> Get(int scheduleId)
        {
            return Task.FromResult(Schedules.FirstOrDefault(x=>x.Id == scheduleId));
        }

        public Task<Schedule> Get(int year, int month)
        {
            return Task.FromResult(Schedules.FirstOrDefault(x=>x.Month == month && x.Year == year));
        }

        public Task UpdateSchedule(Schedule schedule)
        {
            var existsSchedule = Schedules.FirstOrDefault(x=>x.Id == schedule.Id);
            if (existsSchedule != null)
            {
                existsSchedule.CostSubscriptions = schedule.CostSubscriptions;
                existsSchedule.Days = schedule.Days;
                existsSchedule.Year = schedule.Year;
                existsSchedule.Month = schedule.Month;
            }

            return Task.CompletedTask;
        }
    }
}
