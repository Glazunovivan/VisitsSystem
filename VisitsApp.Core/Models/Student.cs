namespace VisitsApp.Core.Models
{
    public class Student
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Отчество
        /// </summary>
        public string Surename { get; set; }

        /// <summary>
        /// Фамиля
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Дата добавления в систему
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Категория скидки, Id
        /// </summary>
        public int? StudentCategoryId { get; set; }
        /// <summary>
        /// Категория скидки
        /// </summary>
        public DiscountCategory? StudentCategory { get; set; }


        /// <summary>
        /// Группа (младшая, старшая), Id
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// Группа (младшая, старшая)
        /// </summary>
        public Group? Group { get; set; }


        public string Fullname => $"{Lastname} {Name} {Surename}";
    }
}
