namespace VisitSchool.Models
{
    /// <summary>
    /// Тип скидки
    /// </summary>
    public class DiscountCategory
    {
        public int Id { get; set; }

        /// <summary>
        /// Скидка в процентах
        /// </summary>
        public double DscountPercent { get; set; } = 0f;
    
    }
}
