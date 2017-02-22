using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTime_Summertime_Standardtime
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt1 = new DateTime(2016, 10, 30, 1, 30, 0);
            var dt2 = dt1.Add(TimeSpan.FromHours(1));
            var dt3 = dt2.Add(TimeSpan.FromHours(1));

            Console.WriteLine($"dt1: {dt1}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt1)}");
            Console.WriteLine($"dt2: {dt2}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt2)}");
            Console.WriteLine($"dt3: {dt3}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt3)}");
            var dts = dt3 - dt1;
            Console.WriteLine($"dt3 - dt1 = {dts}");

            Console.WriteLine();

            var dt1Utc = dt1.ToUniversalTime();
            var dt2Utc = dt2.ToUniversalTime();
            var dt3Utc = dt3.ToUniversalTime();
            var dt2UtcB = dt1Utc.Add(TimeSpan.FromHours(1));
            var dt3UtcB = dt2UtcB.Add(TimeSpan.FromHours(1));

            Console.WriteLine($"dt1Utc: {dt1Utc}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt1Utc)}");
            Console.WriteLine($"dt2Utc: {dt2Utc}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt2Utc)}");
            Console.WriteLine($"dt3Utc: {dt3Utc}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt3Utc)}");
            var dtsUtc = dt3Utc - dt1Utc;
            Console.WriteLine($"dts3Utc - dts1Utc = {dtsUtc}");
            Console.WriteLine();

            Console.WriteLine($"dt1Utc:  {dt1Utc}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt1Utc)}");
            Console.WriteLine($"dt2UtcB: {dt2UtcB}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt2UtcB)}");
            Console.WriteLine($"dt3UtcB: {dt3UtcB}, DST: {TimeZoneInfo.Local.IsDaylightSavingTime(dt3UtcB)}");
            var dtsUtcB = dt3UtcB - dt1Utc;
            Console.WriteLine($"dts3UtcB - dts1UtcB = {dtsUtcB}");

            Console.ReadKey();
        }
    }
}
