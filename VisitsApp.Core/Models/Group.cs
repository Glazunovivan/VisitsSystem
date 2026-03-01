namespace VisitsApp.Core.Models
{
    /// <summary>
    /// Уровень группы (младшая, старшая, секция)
    /// </summary>
    public class Group
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ученики
        /// </summary>
        public List<Student> Students { get; set; } = new();

        /// <summary>
        /// Расписания у группы
        /// </summary>
        public List<Schedule> Schedules { get; set; } = new();
    }
}
