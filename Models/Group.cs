namespace VisitSchool.Models
{
    /// <summary>
    /// Уровень группы (младшая, старшая, секция)
    /// </summary>
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
