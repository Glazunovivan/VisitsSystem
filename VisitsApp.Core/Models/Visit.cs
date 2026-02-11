namespace VisitsApp.Core.Models
{
    /// <summary>
    /// Посещение
    /// </summary>
    public class Visit
    {
        public int Id { get; set; }

        /// <summary>
        /// Расписание
        /// </summary>
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        /// <summary>
        /// День посещения
        /// </summary>
        public int Day { get; set; }    
        public ScheduleDay ScheduleDay { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        /// <summary>
        /// 0 - не был, 1 - был, 2 - был 1 час, 3 - болеет
        /// </summary>
        public int Status { get; set; } 
    }
}
