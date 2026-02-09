using VisitSchool.Dtos;
using VisitSchool.Models;
using VisitSchool.Repositories;

namespace VisitSchool.Services
{
    public class VisitService
    {
        private readonly IVisitsRepository _visitsRepo;
        private readonly IScheduleRepository _scheduleRepo;
        
        public VisitService(IVisitsRepository visitsRepo, IScheduleRepository scheduleRepository) 
        { 
            _visitsRepo = visitsRepo;
            _scheduleRepo = scheduleRepository;
        }

        /// <summary>
        /// Добавить посещение
        /// </summary>
        /// <param name="date"></param>
        public async Task AddOrUpdateVisit(AddStudentVisitDto visit)
        {
            var existVisits = await _visitsRepo.GetVisitByStudentDate(visit.ScheduleId, visit.Day, visit.StudentId);
           
            if (existVisits != null)
            {
                await _visitsRepo.UpdateStatusVisitStudent(visit.ScheduleId, visit.Day, visit.StudentId, visit.Status);
            }
            else
            {
                await _visitsRepo.AddVisit(visit);
            }
        }

        public async Task DeleteVisit(AddStudentVisitDto visit)
        {
            try
            {
                await _visitsRepo.DeleteVisit(visit.ScheduleId, visit.Day);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Получить все посещения по расписанию
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public List<Visit> GetVisitsByScheduleId(int scheduleId)
        {
            return _visitsRepo.GetVisitsByScheduleId(scheduleId).Result;
        }

        /// <summary>
        /// Получить все посещения на дату
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<Visit> GetVisitsByDate(DateTime dateTime)
        {
            var schedule = _scheduleRepo.Get(dateTime.Year, dateTime.Month).Result;
            
            if (_scheduleRepo is InMemoryScheduleRepository)
            {
                _visitsRepo.IncludeSchedule(schedule.Id, schedule);
            }

            var visits = _visitsRepo.GetVisitsByDate(dateTime).Result;
            

            return visits;
        }

        /// <summary>
        /// Исправить посещение
        /// </summary>
        /// <param name="newDateTime"></param>
        /// <param name="studentId"></param>
        public void UpdateVisit(DateTime currentDateTime, DateTime newDateTime, int studentId)
        {
            _visitsRepo.UpdateVisitStudent(currentDateTime, newDateTime, studentId);
        }
    }
}
