using Microsoft.EntityFrameworkCore;
using VisitSchool.DataAccessLayer;

namespace VisitSchool
{
    public partial class App : Application
    {
        private readonly ApplicationContext _context;

        public App(ApplicationContext context)
        {
            InitializeComponent();

            context.Database.Migrate();
            _context = context;
            MainPage = new MainPage();
        }
    }
}
