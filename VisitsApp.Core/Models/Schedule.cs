namespace VisitsApp.Core.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Название расписания
        /// </summary>
        public string ScheduleName { get; set; }

        /// <summary>
        /// Месяц в году
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Стоимость абонемента
        /// </summary>
        public decimal CostSubscriptions { get; set; }

        /// <summary>
        /// Выбранные дни
        /// </summary>
        public List<ScheduleDay?> Days { get; set; }


        /// <summary>
        /// К каким группам 
        /// </summary>
        public List<Group> Groups { get; set; } = new List<Group>();

        /// <summary>
        /// Стоимость занятия
        /// </summary>
        public decimal CostSubscriptionsPerDay => CostSubscriptions / Days.Count;
    }


}
