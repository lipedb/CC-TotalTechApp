using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TotalTechApp.Common
{
    public class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string LoginCompletedKey = "LoginCompleted_key";
        private static readonly bool LoginCompletedDefault = false;

        #endregion

        public static bool LoginCompleted
        {
            get
            {
                return AppSettings.GetValueOrDefault(LoginCompletedKey, LoginCompletedDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LoginCompletedKey, value);
            }
        }
    }
}