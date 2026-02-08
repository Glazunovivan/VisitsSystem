using VisitSchool.Dtos;

namespace VisitSchool.Services
{
    public class CalculateService
    {
        private readonly StudentService _studentService;
        private readonly VisitService _visitsSerivce;
        private readonly ScheduleService _scheduleService;


        public CalculateService(StudentService studentService, VisitService visitService, ScheduleService scheduleService) 
        { 
            _studentService = studentService;
            _visitsSerivce = visitService;
            _scheduleService = scheduleService;
        }

        public async Task<List<CostVisitStudentDto>> CalculateCostVisits(int scheduleId)
        {
            var visits = _visitsSerivce.GetVisitsByScheduleId(scheduleId);
            var students = await _studentService.GetAllStudentsAsync();
            var schedule = await _scheduleService.GetSchedule(scheduleId);
            
            List<CostVisitStudentDto> results = new List<CostVisitStudentDto>();
            foreach (var s in students)
            {
                //посещения ученика
                var visitsStudent = visits.Where(x=>x.StudentId == s.Id);

                //всего отметок
                int countVisit = visitsStudent.Count();

                //количество был 1.5 час / был 1 час
                int countVisitStatus12 = visitsStudent.Count(x=> x.Status == 1 || x.Status == 2);

                //количество не был (пропуск, за него платят)
                int countMissing = schedule.Days.Count - countVisit;

                //количество "болел" - за них не платят деньги
                int countSick = visitsStudent.Count(x => x.Status == 3);

                //todo: доработать скидку
                double discount = 0d;
                if (s.StudentCategory == null)
                {
                    discount = s.StudentCategory?.DscountPercent ?? 0;
                }

                decimal cost = (schedule.CostSubscriptionsPerDay * (countMissing + countVisitStatus12)) * (decimal)(1-discount);

                var value = new CostVisitStudentDto
                {
                    StudentId = s.Id,
                    StudentFullname = s.Fullname,
                    Cost = cost,
                    Discount = discount,
                    CostSubscription = schedule.CostSubscriptions,
                    CountVisit = (countVisit-countSick),
                    CountMissing = countMissing,
                    CountSick = countSick
                };

                results.Add(value);
            }

            return results;
        }
    }
}
