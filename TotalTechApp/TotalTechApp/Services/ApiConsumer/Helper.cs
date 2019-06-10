using System;

namespace TotalTechApp.Services.ApiConsumer
{
    public static class Helper
    {
        private const string API_DATETIME_FORMAT = "yyyy-MM-ddThh:mm:ss";

        public static string FormatDatetime(DateTime dateTime)
        {
            return dateTime.ToString(API_DATETIME_FORMAT);
        }
    }
}
