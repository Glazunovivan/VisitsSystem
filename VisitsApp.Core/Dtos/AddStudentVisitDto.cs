namespace VisitsApp.Core.Dtos
{
    public class AddStudentVisitDto
    {
        public int ScheduleId { get; private set; }
        
        public int StudentId { get; private set; }

        public int Day { get; private set; }

        public int Status { get; private set; } 

        public AddStudentVisitDto()
        {
            
        }

        public AddStudentVisitDto(int scheduleId, int studentId, int day, int status)
        {
            ScheduleId = scheduleId;
            StudentId = studentId;
            Day = day;
            Status = status;
        }
    }
}
