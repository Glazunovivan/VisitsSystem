namespace VisitSchool.Services
{
    public class SettingsService
    {
        public SettingsService()
        {
            
        }

        public string GetVersionApp()
        {
            var version = AppInfo.VersionString;
            var build = AppInfo.BuildString;
            return $"v{version} b{build}";
        }
    }
}
