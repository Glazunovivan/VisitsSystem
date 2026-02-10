namespace VisitSchool.Dtos
{
    public class CostVisitStudentDto
    {
        /// <summary>
        /// Id ученика
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// ФИО ученика
        /// </summary>
        public string FullnameStudent {  get; set; }    

        /// <summary>
        /// ФИО ученика
        /// </summary>
        public string StudentFullname { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Количество дней в расписании
        /// </summary>
        public int CountDay { get; set; }

        /// <summary>
        /// Количество посещений
        /// </summary>
        public int CountVisit {  get; set; }    

        /// <summary>
        /// Количество пропусков
        /// </summary>
        public int CountMissing { get; set; }

        /// <summary>
        /// Количество дней отсуствия по болезни
        /// </summary>
        public int CountSick { get; set; }

        /// <summary>
        /// Стоимость абонемента
        /// </summary>
        public decimal CostSubscription { get; set; }

        /// <summary>
        /// Скидка в процентах
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// Название расписания
        /// </summary>
        public string ScheduleName { get; set; }


        public CostVisitStudentDto()
        {
            
        }
    }
}
