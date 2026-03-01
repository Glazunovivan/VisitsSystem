using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VisitsApp.Data.SQLite;

namespace VisitsApp.Data.DbMigrator
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            // !!! ВАЖНО: Здесь ты указываешь строку подключения для миграций.
            // В реальном приложении она может быть из appsettings.json или переменной окружения.
            // Для миграций можно использовать тестовую или дефолтную строку.
            // Убедись, что путь к файлу БД корректен для места, откуда ты запускаешь миграции.
            optionsBuilder.UseSqlite("Data Source=visits.db");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
