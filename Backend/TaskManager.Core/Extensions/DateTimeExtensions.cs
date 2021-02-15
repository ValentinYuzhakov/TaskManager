using System;

namespace TaskManager.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDefault(this DateTime dateTime)
        {
            return dateTime == default
                ? true
                : false;
        }
    }
}
