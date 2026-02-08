using VisitSchool.Dtos;
using VisitSchool.Models;

namespace VisitSchool.Repositories
{
    public interface IVisitsRepository
    {
        /// <summary>
        /// Добавить посещение ученика
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task AddVisit(AddStudentVisitDto visit);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<Visit> GetVisitByDate(DateTime dateTime);


        Task<List<Visit>> GetVisitsByDate(DateTime dateTime);

        /// <summary>
        /// Все посещения
        /// </summary>
        /// <returns></returns>
        Task<List<Visit>> GetAllVisitsAsync();

        /// <summary>
        /// Все посещения за период
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<Visit>> GetAllVisitsAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Все посещения ученика
        /// </summary>
        /// <returns></returns>
        Task<List<Visit>> GetVisitByStudentId();

        /// <summary>
        /// Исправить посещение
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task UpdateVisitStudent(DateTime currentDateTime, DateTime newDateTime, int studentId);

        Task<List<Visit>> GetVisitsByScheduleId(int scheduleId);

        Task IncludeSchedule(int scheduleId, Schedule schedule);
    }
}
