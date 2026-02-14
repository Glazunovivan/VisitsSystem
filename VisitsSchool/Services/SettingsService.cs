using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VisitSchool.DataAccessLayer;

namespace VisitSchool.Services
{
    public class SettingsService
    {
        private readonly ApplicationContext _dbContext;

        public SettingsService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public string GetVersionApp()
        {
            var version = AppInfo.VersionString;
            var build = AppInfo.BuildString;
            return $"v{version} b{build}";
        }

        public string GetDbPath() => _dbContext.DbPath;

        public async Task<bool> ClearDataBase()
        {
            // 1. Закрываем соединение, чтобы SQLite отпустил файл
            await _dbContext.Database.CloseConnectionAsync();

            // 2. Удаляем файл базы данных
            if (File.Exists(_dbContext.DbPath))
            {
                // 2. Очищаем все пулы соединений SQLite
                // Это "магическая команда", которая заставляет SQLite отпустить файл
                SqliteConnection.ClearAllPools();

                File.Delete(_dbContext.DbPath);
            }
            else
            {
                throw new ArgumentNullException($"Не удалось определить путь к базе данных... Удалить файл вручную. Сохраненный путь: {_dbContext.DbPath}");
            }

            await _dbContext.Database.MigrateAsync();

            return true;
        }
    }
}
