namespace VisitsApp.Core.Models.Enums
{
    public enum AttendanceStatus
    {
        /// <summary>
        /// не был
        /// </summary>
        None = 0,
        /// <summary>
        /// был 1.5 час
        /// </summary>
        Full = 1,
        /// <summary>
        /// был 1 час
        /// </summary>
        OneHour = 2,
        /// <summary>
        /// болеет
        /// </summary>
        Sick = 3
    }
}
