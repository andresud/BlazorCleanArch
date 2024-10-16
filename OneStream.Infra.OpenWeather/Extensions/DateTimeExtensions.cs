﻿namespace OneStream.Infra.OpenWeather.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTime(this long unixTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime).ToLocalTime();
        }
    }
}
