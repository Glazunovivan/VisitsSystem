using Microsoft.Extensions.Logging;
using VisitsApp.Core.Dtos;
using VisitsApp.Core.Models;
using VisitsApp.Core.Repositories;

namespace VisitsApp.Core.Services
{
    public class VisitService
    {
        private readonly ILogger<VisitService> _logger;
        private readonly IVisitsRepository _visitsRepo;
        private readonly IScheduleRepository _scheduleRepo;
        
        public VisitService(IVisitsRepository visitsRepo, IScheduleRepository scheduleRepository, ILogger<VisitService> logger) 
        { 
            _visitsRepo = visitsRepo;
            _scheduleRepo = scheduleRepository;
            _logger = logger;
        }

        /// <summary>
        /// Добавить посещение
        /// </summary>
        /// <param name="date"></param>
        public async Task AddOrUpdateVisit(AddStudentVisitDto visit)
        {
            Visit existVisits = null;
            try
            {
                _logger.LogInformation($"Поиск существующего посещения ScheduleId={visit.ScheduleId} Day={visit.Day} StudentId={visit.StudentId}");

                existVisits = await _visitsRepo.GetVisitByStudentDate(visit.ScheduleId, visit.Day, visit.StudentId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}");
                throw ex;
            }
           
            if (existVisits != null)
            {
                try
                {
                    _logger.LogInformation("Обновляем посещение...");
                    await _visitsRepo.UpdateStatusVisitStudent(visit.ScheduleId, visit.Day, visit.StudentId, visit.Status);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    _logger.LogInformation("Add новое посещение");
                    await _visitsRepo.AddVisit(visit);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}");
                    throw ex;
                }
            }
        }

        public async Task DeleteVisit(AddStudentVisitDto visit)
        {
            try
            {
                _logger.LogInformation($"Удаляем посещение ScheduleId={visit.ScheduleId}, Day = {visit.Day}");
                await _visitsRepo.DeleteVisit(visit.ScheduleId, visit.Day);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}");
                throw ex;
            }
        }

        /// <summary>
        /// Получить все посещения по расписанию
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public List<Visit> GetVisitsByScheduleId(int scheduleId)
        {
            try
            {
                return _visitsRepo.GetVisitsByScheduleId(scheduleId).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}", "Не удалось получить список посещений");
                throw ex;
            }
        }

        /// <summary>
        /// Получить все посещения на дату
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<List<Visit>> GetVisitsByDate(DateTime dateTime)
        {
            try
            {
                var schedule = await _scheduleRepo.Get(dateTime.Year, dateTime.Month);

                if (_scheduleRepo is InMemoryScheduleRepository)
                {
                    await _visitsRepo.IncludeSchedule(schedule.Id, schedule);
                }

                var visits = await _visitsRepo.GetVisitsByDate(dateTime);

                return visits;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}", "Не удалось получить список посещений по дате");
                throw ex;
            }

        }

        /// <summary>
        /// Исправить посещение
        /// </summary>
        /// <param name="newDateTime"></param>
        /// <param name="studentId"></param>
        public void UpdateVisit(DateTime currentDateTime, DateTime newDateTime, int studentId)
        {
            try
            {
                _visitsRepo.UpdateVisitStudent(currentDateTime, newDateTime, studentId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}. {currentDateTime}, {newDateTime}, {studentId}", "Не удалось обновить посещение");
            }
        }


        public async Task DeleteVisits(int scheduleId)
        {
            try
            {
                await _visitsRepo.DeleteAllVisits(scheduleId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}.");
                throw ex;
            }
        }
    }
}
