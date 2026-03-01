using Microsoft.Extensions.Logging;
using VisitsApp.Core.Dtos;
using VisitsApp.Core.Models;

namespace VisitsApp.Core.Services
{
    public class CalculateService
    {
        private readonly StudentService _studentService;
        private readonly VisitService _visitsSerivce;
        private readonly ScheduleService _scheduleService;
        private readonly ILogger<CalculateService> _logger;


        public CalculateService(StudentService studentService, VisitService visitService, ScheduleService scheduleService, ILogger<CalculateService> logger) 
        { 
            _studentService = studentService;
            _visitsSerivce = visitService;
            _scheduleService = scheduleService;
            _logger = logger;
        }

        /// <summary>
        /// Рассчитать оплату за посещения без учета перерасчёта
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<List<CostVisitStudentDto>> CalculateCostVisits(int scheduleId)
        {
            try
            {
                var visits = _visitsSerivce.GetVisitsByScheduleId(scheduleId);
                var students = await _studentService.GetAllStudentsAsync();
                var schedule = await _scheduleService.GetScheduleAsync(scheduleId);

                List<CostVisitStudentDto> results = new List<CostVisitStudentDto>();
                foreach (var s in students)
                {
                    //посещения ученика
                    var visitsStudent = visits.Where(x => x.StudentId == s.Id);

                    //всего отметок
                    int countVisit = visitsStudent.Count();

                    //количество был 1.5 час / был 1 час
                    int countVisitStatus12 = visitsStudent.Count(x => x.Status == 1 || x.Status == 2);

                    //количество не был (пропуск, за него платят)
                    int countMissing = schedule.Days.Count - countVisit;

                    //количество "болел" - за них не платят деньги
                    int countSick = visitsStudent.Count(x => x.Status == 3);

                    double discount = 0d;
                    if (s.StudentCategory != null)
                    {
                        discount = (s.StudentCategory?.DscountPercent ?? 0) / 100;
                    }

                    decimal cost = (schedule.CostSubscriptionsPerDay * (countMissing + countVisitStatus12)) * (decimal)(1 - discount);

                    var value = new CostVisitStudentDto
                    {
                        StudentId = s.Id,
                        StudentFullname = s.Fullname,
                        Cost = cost,
                        Discount = discount,
                        CostSubscription = schedule.CostSubscriptions,
                        CountVisit = (countVisit - countSick),
                        CountMissing = countMissing,
                        CountSick = countSick,
                        CountDay = schedule.Days.Count,
                        FullnameStudent = s.Fullname,
                        ScheduleName = schedule.ScheduleName
                    };

                    results.Add(value);
                }

                return results;

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Рассчитать стоимость занятий с учетом перерасчета
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="scheduleRecalcId">Месяц для перерасчета, Id</param>
        /// <returns></returns>
        public async Task<List<CostVisitStudentDto>> CalculateCostVisits(int scheduleId, int scheduleRecalcId)
        {
            try
            {
                var results=  new List<CostVisitStudentDto>();  
                var schedule = await _scheduleService.GetScheduleAsync(scheduleId);
                if (schedule == null)
                {
                    throw new ArgumentNullException($"Расписание с Id {scheduleId} не найдено");
                }

                List<Student> students = new List<Student>();
                //если группы для расписания заданы, тогда берем из групп
                if (schedule.Groups != null)
                {
                    foreach (var g in schedule.Groups)
                    {
                        students.AddRange(g.Students);
                    }
                }
                //берем всех
                else
                {
                    var st = await _studentService.GetAllStudentsAsync();
                    students.AddRange(st);
                }

                var visits = _visitsSerivce.GetVisitsByScheduleId(scheduleId);
                var visitsRecalc = _visitsSerivce.GetVisitsByScheduleId(scheduleRecalcId);
                var scheduleRecalc = await _scheduleService.GetScheduleAsync(scheduleRecalcId);

                foreach (var s in students)
                {
                    //посещения ученика
                    var visitsStudent = visits.Where(x => x.StudentId == s.Id);

                    //всего отметок
                    int countVisit = visitsStudent.Count();

                    //количество был 1.5 час / был 1 час
                    int countVisitStatus12 = visitsStudent.Count(x => x.Status == 1 || x.Status == 2);

                    //количество не был (пропуск, за него платят)
                    int countMissing = schedule.Days.Count - countVisit;

                    //количество "болел" - за них не платят деньги
                    int countSick = visitsStudent.Count(x => x.Status == 3);

                    //количество "болел" в прошлом месяце (учитывается в перерасчете)
                    int countSickRecalc = visitsRecalc.Count(x=>x.Status == 3);

                    double discount = 0d;
                    if (s.StudentCategory != null)
                    {
                        discount = (s.StudentCategory?.DscountPercent ?? 0) / 100;
                    }

                    //перерасчет по цене прошлого месяца (+ скидка)
                    decimal recalcCost = scheduleRecalc.CostSubscriptionsPerDay * countSickRecalc * (decimal)(1-discount);

                    //todo: уточнить как скидку применять
                    decimal cost = ((schedule.CostSubscriptionsPerDay * (countMissing + countVisitStatus12)) * (decimal)(1 - discount)) - recalcCost;

                    var value = new CostVisitStudentDto
                    {
                        StudentId = s.Id,
                        StudentFullname = s.Fullname,
                        Cost = cost,
                        Discount = discount,
                        CostSubscription = schedule.CostSubscriptions,
                        CountVisit = (countVisit - countSick),
                        CountMissing = countMissing,
                        CountSick = countSick,
                        CountDay = schedule.Days.Count,
                        FullnameStudent = s.Fullname,
                        ScheduleName = schedule.ScheduleName
                    };

                    if (scheduleRecalc != null)
                    {
                        value.NameRecalc = scheduleRecalc.ScheduleName;
                        value.CostRecalc = recalcCost;
                    }

                    results.Add(value);
                }
                
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
