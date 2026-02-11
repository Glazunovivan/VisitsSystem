namespace VisitsApp.Core.Models
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

        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; } = string.Empty;
    
    }
}
