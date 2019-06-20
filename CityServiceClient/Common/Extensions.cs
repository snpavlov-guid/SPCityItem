using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityServiceClient.Common
{
    public static class Extensions
    {
        #region Unix time
        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }

        #endregion

        #region Exceptions

        public static string GetAggregatedExceptionMessage(this Exception ex)
        {
            var message = ex.Message;

            if (!(ex is AggregateException))
            {
                return message;
            }

            var sb = new StringBuilder();

            var agex = ((AggregateException)ex).Flatten();

            foreach (var e in agex.InnerExceptions)
            {
                sb.AppendLine(e.Message);
            }

            message = sb.ToString();

            return message;
        }

        #endregion
    }
}
