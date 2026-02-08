using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using VisitSchool.DataAccessLayer;
using VisitSchool.Repositories;
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
            using var stream = FileSystem.OpenAppPackageFileAsync("appsetings.json").Result;

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

            //dbcontext
            builder.Services.AddDbContext<ApplicationContext>();

            //repositories
            builder.Services.AddScoped<IStudentRepository, StudentsRepository>();
            builder.Services.AddScoped<IVisitsRepository, VisitsRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

            //services
            builder.Services.AddScoped<GroupService>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<VisitService>();
            builder.Services.AddScoped<SettingsService>();
            builder.Services.AddScoped<CalculateService>();
            builder.Services.AddScoped<ScheduleService>();


            return builder.Build();
        }
    }
}
