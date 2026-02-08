namespace VisitSchool.Models
{
    public class ScheduleDay
    {
        public int Id { get; set; } 

        public int Day { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
