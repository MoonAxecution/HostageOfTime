using System;

namespace HOT
{
    public static class DateUtils
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static int SecondsInDay = 86400;
        
        public static DateTime FromUnixTimestamp(int timestamp)
        {
            return unixEpoch.AddSeconds(timestamp);
        }

        public static int ToUnixTimestamp(this DateTime dt)
        {
            return (int) (dt - unixEpoch).TotalSeconds;
        }

        public static int GetEndTime(int time) => DateTime.Now.ToUnixTimestamp() + time;
    }
}