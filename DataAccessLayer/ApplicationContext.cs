using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VisitSchool.Models;

namespace VisitSchool.DataAccessLayer
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Ученики
        /// </summary>
        public DbSet<Student> Students => Set<Student>(); 
        /// <summary>
        /// Группы
        /// </summary>
        public DbSet<Group> Groups => Set<Group>();
        /// <summary>
        /// Скидки
        /// </summary>
        public DbSet<DiscountCategory> StudentCategories => Set<DiscountCategory>();
        /// <summary>
        /// Сетка расписания
        /// </summary>
        public DbSet<Schedule> Schedules => Set<Schedule>();
        /// <summary>
        /// Дни в расписании
        /// </summary>
        public DbSet<Schedule> Days => Set<Schedule>();
        /// <summary>
        /// Посещения
        /// </summary>
        public DbSet<Visit> Visits => Set<Visit>();

#if !MIGRATIONDB
        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
#endif

#if MIGRATIONDB
        public ApplicationContext()
        {
        }
#endif

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if !MIGRATIONDB
            var pathDbConfig = _configuration.GetConnectionString("DefaultConnection");
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, pathDbConfig);

            optionsBuilder.UseSqlite($"Filename = {dbPath}");
#endif

#if MIGRATIONDB
            optionsBuilder.UseSqlite("Data Source=visits.db");
#endif
        }
    }
}
