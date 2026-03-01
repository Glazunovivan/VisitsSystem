using Microsoft.EntityFrameworkCore;
using VisitsApp.Data.SQLite;

namespace VisitSchool
{
    public partial class App : Application
    {
        private readonly ApplicationContext _context;

        public App(ApplicationContext context)
        {
            InitializeComponent();

            try
            {
                context.Database.Migrate();
            }
            //миграции уже применены
            catch (Exception ex) { }
            _context = context;
            MainPage = new MainPage();
        }
    }
}
