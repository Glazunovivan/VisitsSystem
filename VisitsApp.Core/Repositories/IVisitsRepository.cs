using VisitsApp.Core.Dtos;
using VisitsApp.Core.Models;

namespace VisitsApp.Core.Repositories
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
        Task<List<Visit>> GetVisitByStudentId(int id);

        /// <summary>
        /// Исправить посещение
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task UpdateVisitStudent(DateTime currentDateTime, DateTime newDateTime, int studentId);

        /// <summary>
        /// Обновить статус посещения
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="studentId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task UpdateStatusVisitStudent(int scheduleId, int day, int studentId, int status);

        /// <summary>
        /// Получить все посещения по Id расписания
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        Task<List<Visit>> GetVisitsByScheduleId(int scheduleId);

        Task IncludeSchedule(int scheduleId, Schedule schedule);

        /// <summary>
        /// Удалить посещение
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        Task DeleteVisit(int scheduleId, int day);

        /// <summary>
        /// Удалить все посещения
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        Task DeleteAllVisits(int scheduleId);

        /// <summary>
        /// Получить посещение ученика по дате
        /// </summary>
        /// <returns></returns>
        Task<Visit> GetVisitByStudentDate(int scheduleId, int day, int studentId);
    }
}
