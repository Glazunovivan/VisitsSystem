using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;
using VisitsApp.Core.Repositories;
using VisitsApp.Core.Services;
using VisitSchool.DataAccessLayer;
using VisitSchool.Repositories.SQLite;
using VisitSchool.Services;

namespace VisitSchool
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = FileSystem.OpenAppPackageFileAsync("appsetings.txt").Result;

            // Создаем конфигурацию
            var config = new ConfigurationBuilder()
                            .AddJsonStream(stream)
                            .Build();

            // Добавляем конфигурацию в DI-контейнер
            builder.Configuration.AddConfiguration(config);

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            try
            {

                var logPath = Path.Combine(FileSystem.AppDataDirectory, "logs", "log-.txt");
                //настройка serilog
                Log.Logger = new LoggerConfiguration()
                                .Enrich.FromLogContext()    //контекст
                                .Enrich.WithProperty("Application", "VisitSchool")
                                .MinimumLevel.Debug()
                                .WriteTo.File(logPath,
                                              rollingInterval: RollingInterval.Day,
                                              retainedFileCountLimit: 7,
                                              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                                .CreateLogger();

                builder.Logging.AddSerilog(dispose: true);

                builder.Services.AddSingleton(new LogFileInfo(logPath));
                //dbcontext
                builder.Services.AddDbContext<ApplicationContext>();

                //repositories
                builder.Services.AddScoped<IStudentRepository, StudentsRepository>();
                builder.Services.AddScoped<IVisitsRepository, VisitsRepository>();
                builder.Services.AddScoped<IGroupRepository, GroupRepository>();
                builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
                builder.Services.AddScoped<IDiscountCategoryRepository, DiscountCategories>();

                //services
                builder.Services.AddScoped<GroupService>();
                builder.Services.AddScoped<DiscountCategoryService>();
                builder.Services.AddScoped<StudentService>();
                builder.Services.AddScoped<VisitService>();
                builder.Services.AddScoped<SettingsService>();
                builder.Services.AddScoped<CalculateService>();
                builder.Services.AddScoped<ScheduleService>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.InnerException?.Message);
            }

            return builder.Build();
        }
    }
}
