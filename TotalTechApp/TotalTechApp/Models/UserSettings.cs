using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TotalTechApp.Models
{
    /// <summary>  
    /// This is the Settings static class that can be used in your Core solution or in any  
    /// of your client applications. All settings are laid out the same exact way with getters  
    /// and setters.   
    /// </summary>  
    public static class UserSettings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string EmailAddress
        {
            get => AppSettings.GetValueOrDefault(nameof(EmailAddress), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(EmailAddress), value);
        }

        public static string UserList
        {
            get => AppSettings.GetValueOrDefault(nameof(UserList), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserList), value);
        }

        public static void ClearAllData()
        {
            AppSettings.Clear();
        }

    }
}
