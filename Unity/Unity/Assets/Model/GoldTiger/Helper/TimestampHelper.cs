using System;

namespace ETModel
{
    public static class TimestampHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        public static string GetTimeStamp()
        {
            long ts = ConvertDateTimeToInt(DateTime.Now);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        private static long ConvertDateTimeToInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
    }
}
