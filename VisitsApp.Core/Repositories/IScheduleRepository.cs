using VisitsApp.Core.Models;

namespace VisitsApp.Core.Repositories
{
    public interface IScheduleRepository
    {
        Task<Schedule> Add(Schedule schedule);

        Task<List<Schedule>> GetAll();

        Task<Schedule> Get(int scheduleId);

        Task<Schedule> Get(int year, int month);

        Task UpdateSchedule(Schedule schedule);

        Task DeleteSchedule(int scheduleId);
    }
}
