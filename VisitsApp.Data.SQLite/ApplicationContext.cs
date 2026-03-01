using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using VisitsApp.Core.Models;

namespace VisitsApp.Data.SQLite
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public string DbPath { get; private set; }

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
        public DbSet<ScheduleDay> Days => Set<ScheduleDay>();
        
        /// <summary>
        /// Посещения
        /// </summary>
        public DbSet<Visit> Visits => Set<Visit>();

        // Этот конструктор важен для EF Core CLI
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

#if MIGRATIONDB
        public ApplicationContext()
        {
        }
#endif

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//#if !MIGRATIONDB
//            //var pathDbConfig = _configuration.GetConnectionString("DefaultConnection");
//            //var dbPath = Path.Combine(FileSystem.AppDataDirectory, "visits.db");

//            //optionsBuilder.UseSqlite($"Filename={dbPath}");
//            //DbPath = dbPath;
//#endif

            //TODO: перенести в конфиги
            optionsBuilder.UseSqlite("Data Source=visits.db");
        }
    }
}
