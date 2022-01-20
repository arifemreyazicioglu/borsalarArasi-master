using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.HelpersBinance
{
    public static class TimeHelperBinance
    {
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return Convert.ToInt64((date - epoch).TotalMilliseconds- 10800000);
        }
    }
}
