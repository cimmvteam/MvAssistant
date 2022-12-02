using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MvaCToolkitCs.v1_2.Timing
{
    public class CtkTimeUtil
    {
        //ToUniversalTime/ToLocalTime 會自動判別Kind = Local / Utc 來決定加減
        //若為Unspecified, 則可當兩者,
        // toLocal: +8 & Kink = Local
        // toUniversal: -8 & Kind = Utc



        //--- DateTime and Timestamp converter ---------

        //--- ROC ---------
        const int YearDiffBetweenRocAndAd = 1911;



        public static DateTime? ConvertToDateTime(string datetime, string srcFormat)
        {
            DateTime rsdate;
            if (DateTime.TryParse(datetime, out rsdate)
                ||

                DateTime.TryParseExact(datetime, srcFormat
                    , CultureInfo.InvariantCulture
                    , DateTimeStyles.None
                    , out rsdate))
            { return rsdate; }
            return null;
        }

       

        #region Week Operation
        //--- Week ---------

        /// <summary>
        /// 不超過當前日期的 dow(Day Of Week) (e.q.周二) 是哪天
        /// </summary>
        /// <param name="dow"></param>
        /// <returns></returns>
        public static DateTime GetLastDow(DayOfWeek dow) { return GetLastDow(dow, DateTime.Now); }
        public static DateTime GetLastDow(DayOfWeek dow, DateTime date)
        {

            var rs = date.AddDays((int)dow - (int)date.DayOfWeek);

            //如果超過當前日期, 就把它減回來
            if (rs > date)
                rs = rs.AddDays(-7);

            return rs;
        }
        public static DateTime GetThisDow(DayOfWeek dow) { return GetThisDow(dow, DateTime.Now); }
        public static DateTime GetThisDow(DayOfWeek dow, DateTime date) { return date.AddDays((int)dow - (int)date.DayOfWeek); }
        public static DateTime GetWeeklyEnd(DateTime date)
        {
            var last = GetThisDow(DayOfWeek.Saturday, date);
            return last;
        }
        public static DateTime GetWeeklyEndInSameYear(DateTime date)
        {
            var last = GetWeeklyEnd(date);
            if (last.Year > date.Year) last = new DateTime(date.Year, 12, 31);
            return last;
        }
        public static DateTime GetWeeklyStart(DateTime date)
        {
            var first = GetThisDow(DayOfWeek.Sunday, date);
            return first;
        }
        public static DateTime GetWeeklyStartInSameYear(DateTime date)
        {
            var first = GetWeeklyStart(date);
            if (first.Year < date.Year) first = new DateTime(date.Year, 1, 1);
            return first;
        }
        public static int WeekOfYear(DateTime date)
        {
            return CultureInfo
               .InvariantCulture
               .Calendar
               .GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        #endregion

        #region Month Operation

        public static DateTime GetFirstDayOfMonth(DateTime dt) { return new DateTime(dt.Year, dt.Month, 1); }
        public static DateTime GetLastDayOfMonth(DateTime dt) { return GetFirstDayOfMonth(dt).AddMonths(1).AddDays(-1); }

        #endregion

        #region Year Operation

        public static int HalfOfYear(DateTime dt) { return (dt.Month - 1) / 6 + 1; }
        public static int QuarterOfYear(DateTime dt) { return (dt.Month - 1) / 3 + 1; }

        #endregion

        #region Transfer Date Time

        /// <summary>
        /// 取得下一個的日
        /// </summary>
        public static DateTime GetNextDay(DateTime dt, int day = 0, bool isIncludeToday = true)
        {
            var diff = day - dt.Day;//上一個期望時間, 若為正, 就代表己越過零點
            var mydt = dt;
            if (isIncludeToday && diff < 0)
                mydt.AddMonths(1);
            else if (!isIncludeToday && diff <= 0)
                mydt.AddMonths(1);

            return new DateTime(mydt.Year, mydt.Month, day);
        }

        /// <summary>
        /// 取得過往的指定時分秒
        /// </summary>
        public static DateTime GetNextTime(DateTime dt, int hour = 0, int minute = 0, int second = 0, bool isIncludeToday = true)
        {
            var diff = hour - dt.Hour;//上一個期望時間, 若為正, 就代表己越過零點
            var mydt = dt;
            if (isIncludeToday && diff < 0)
                mydt = dt.AddHours(24);
            else if (!isIncludeToday && diff <= 0)
                mydt = dt.AddHours(24);
            return new DateTime(mydt.Year, mydt.Month, mydt.Day, hour, minute, second);
        }

        /// <summary>
        /// 取得己過往的日
        /// </summary>
        public static DateTime GetPrevDay(DateTime dt, int day = 0, bool isIncludeToday = true)
        {
            var diff = day - dt.Day;//上一個期望時間, 若為正, 就代表己越過零點
            var mydt = dt;
            if (isIncludeToday && diff > 0)
                mydt.AddMonths(-1);
            else if (!isIncludeToday && diff >= 0)
                mydt.AddMonths(-1);

            return new DateTime(mydt.Year, mydt.Month, day);
        }

        /// <summary>
        /// 取得過往的指定時分秒
        /// </summary>
        public static DateTime GetPrevTime(DateTime? dt, int hour = 0, int minute = 0, int second = 0, bool isIncludeToday = true)
        {
            var mydt = dt.HasValue ? dt.Value : DateTime.Now;

            var diff = hour - mydt.Hour;//上一個期望時間, 若為正, 就代表己越過零點
            if (isIncludeToday && diff > 0)
                mydt = mydt.AddHours(-24);
            else if (!isIncludeToday && diff >= 0)
                mydt = mydt.AddHours(-24);
            return new DateTime(mydt.Year, mydt.Month, mydt.Day, hour, minute, second);
        }



        #endregion




        #region Normal DateTime / String
        /*[d20220627] 一般日期時間字串 = 沒有符號
         */

        public static DateTime DateTimeParseExact(string s, string format = "yyyyMMdd") { return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture); }
        public static DateTime DateTimeParseExact(string s, DateTime defaultDt, string format = "yyyyMMdd")
        {
            var dt = defaultDt;
            DateTimeTryParseExact(s, out dt);
            return dt;
        }
        public static bool DateTimeTryParseExact(string s, out DateTime result, string format = "yyyyMMdd") { return DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result); }

        public static DateTime FromDTime(string s) { return FromYyyyMmDdHhIiSs(s); }
        public static DateTime? FromDTimeOrDefault(string s)
        {
            try { return FromYyyyMmDdHhIiSs(s); }
            catch (Exception) { return null; }
        }
        public static bool FromDTimeTry(string s, out DateTime dt) { return FromYyyyMmDdHhIiSsTry(s, out dt); }
        public static DateTime? FromDTimeTryOrDefault(string s)
        {
            var dt = new DateTime();
            if (FromYyyyMmDdHhIiSsTry(s, out dt)) return dt;
            return null;
        }
        public static DateTime FromYyyy(string s, int month = 1, int day = 1)
        {
            var dt = DateTimeParseExact(s, "yyyy");
            return new DateTime(dt.Year, month, day);
        }
        public static DateTime FromYyyyHy(string yyyyhyhy, int day = 1)
        {
            var yyyy = Convert.ToInt32(yyyyhyhy.Substring(0, 4));
            var hyhy = Convert.ToInt32(yyyyhyhy.Substring(4));

            var date = new DateTime(yyyy, 1, day);
            date = date.AddMonths((hyhy - 1) * 6);

            var realYyyyHyhy = ToYyyyHy(date);
            if (yyyyhyhy != realYyyyHyhy) throw new InvalidOperationException();

            return date;
        }
        public static DateTime FromYyyyMm(string s) { return DateTimeParseExact(s, "yyyyMM"); }
        public static DateTime FromYyyyMmDd(string s) { return DateTimeParseExact(s, "yyyyMMdd"); }
        public static DateTime FromYyyyMmDdHh(string s) { return DateTimeParseExact(s, "yyyyMMddHH"); }

        public static DateTime FromYyyyMmDdHhIi(string s) { return DateTimeParseExact(s, "yyyyMMddHHmm"); }

        public static DateTime FromYyyyMmDdHhIiSs(string s) { return DateTimeParseExact(s, "yyyyMMddHHmmss"); }

        public static bool FromYyyyMmDdHhIiSsTry(string s, out DateTime dt) { return DateTimeTryParseExact(s, out dt, "yyyyMMddHHmmss"); }

        public static DateTime? FromYyyyMmDdNullable(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            return new Nullable<DateTime>(FromYyyyMmDd(s));
        }
        public static bool FromYyyyMmDdTry(string s, out DateTime dt) { return DateTimeTryParseExact(s, out dt, "yyyyMMdd"); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yyyyqq"></param>
        /// <returns>該季第一天</returns>
        public static DateTime FromYyyyQq(string yyyyqq, int day = 1)
        {
            var yyyy = Convert.ToInt32(yyyyqq.Substring(0, 4));
            var qq = Convert.ToInt32(yyyyqq.Substring(4));

            var date = new DateTime(yyyy, 1, day);
            date = date.AddMonths((qq - 1) * 3);

            var realYyyyQq = ToYyyyQq(date);
            if (yyyyqq != realYyyyQq) throw new InvalidOperationException();

            return date;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yyyyww"></param>
        /// <returns>該周的某天</returns>
        public static DateTime FromYyyyWw(string yyyyww)
        {

            var yyyy = Convert.ToInt32(yyyyww.Substring(0, 4));
            var ww = Convert.ToInt32(yyyyww.Substring(4));

            var date = new DateTime(yyyy, 1, 1);
            date = date.AddDays(7 * ww - 7);

            var realYyyyww = ToYyyyWw(date);
            if (yyyyww != realYyyyww) throw new InvalidOperationException();

            return date;
        }
        public static DateTime FromYyyyWw(string yyyyww, DayOfWeek dow) { return GetThisDow(DayOfWeek.Saturday, FromYyyyWw(yyyyww)); }
        public static DateTime? FromYyyyWwNullable(string yyyyww)
        {
            if (string.IsNullOrEmpty(yyyyww)) return null;
            return new Nullable<DateTime>(FromYyyyWw(yyyyww));
        }
        public static DateTime? FromYyyyWwNullable(string yyyyww, DayOfWeek dow)
        {
            if (string.IsNullOrEmpty(yyyyww)) return null;
            return new Nullable<DateTime>(FromYyyyWw(yyyyww, dow));
        }

        public static string ToDTime(DateTime dt) { return ToYyyyMmDdHhIiSs(dt); }
        public static string ToYyyy(DateTime dt) { return dt.ToString("yyyy"); }
        public static string ToYyyyHy(DateTime dt)
        {
            var Hyhy = HalfOfYear(dt);
            return string.Format("{0}{1:00}", dt.ToString("yyyy"), Hyhy);
        }
        public static string ToYyyyMm(DateTime dt) { return dt.ToString("yyyyMM"); }
        public static string ToYyyyMm(DateTime? dt) { return dt.HasValue ? ToYyyyMm(dt.Value) : null; }
        public static string ToYyyyMmDd(DateTime dt) { return dt.ToString("yyyyMMdd"); }
        public static string ToYyyyMmDd(DateTime? dt) { return dt.HasValue ? ToYyyyMmDd(dt.Value) : null; }
        public static string ToYyyyMmDdHh(DateTime dt) { return dt.ToString("yyyyMMddHH"); }
        public static string ToYyyyMmDdHhIi(DateTime dt) { return dt.ToString("yyyyMMddHHmm"); }
        public static string ToYyyyMmDdHhIiSs(DateTime dt) { return dt.ToString("yyyyMMddHHmmss"); }
        public static string ToYyyyQq(DateTime dt)
        {
            var qq = QuarterOfYear(dt);
            return string.Format("{0}{1:00}", dt.ToString("yyyy"), qq);
        }
        public static string ToYyyyWw(DateTime dt)
        {
            var weekOfYear = CtkTimeUtil.WeekOfYear(dt);
            return string.Format("{0}{1:00}", dt.ToString("yyyy"), weekOfYear);
        }
        public static string ToYyyyWw(DateTime? dt) { return dt.HasValue ? ToYyyyWw(dt.Value) : null; }

        #endregion



        #region Sign DateTime / String : 一代目, prefix 1 字元

        /*[d20210327]
         * Normal DateTime: 一般轉換, Func Name 直接 代表轉換格式
         * Sign DateTime: 帶符號轉換, 自定義格式, 有分 1/3/6字元符號 及 完整符號轉換
         *  1字元: 用 1個 字母代表該時間週期, 後面接數字 (沒有underline)
         *  3字元: 用 3個 字母代表該時間週期, 不足補 underline 在符號和數字之間
         *  6字元: 用 6個 字母代表該時間週期, 不足補 underline 在符號和數字之間
         *  完整: 寫出完整週期符號稱呼, 後面接數字 (沒有underline)
         * Timestammp: 已被定義成通用時間戳記
         * Mark: 不像是時間相關的用詞
         */


        public static DateTime FromSign1Day(string yyyymmdd)
        {
            if (!yyyymmdd.StartsWith("d")) throw new ArgumentException("錯誤的Sign");
            yyyymmdd = yyyymmdd.Substring(1);
            return FromYyyyMmDd(yyyymmdd);
        }
        public static DateTime FromSign1Month(string yyyymm)
        {
            if (!yyyymm.StartsWith("m")) throw new ArgumentException("錯誤的Sign");
            yyyymm = yyyymm.Substring(1);
            return FromYyyyMm(yyyymm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yyyyqq"></param>
        /// <returns>該季第一天</returns>
        public static DateTime FromSign1Quarter(string yyyyqq)
        {
            if (!yyyyqq.StartsWith("q")) throw new ArgumentException("錯誤的Sign");
            yyyyqq = yyyyqq.Substring(1);
            return FromYyyyQq(yyyyqq);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yyyyww"></param>
        /// <returns>該周的某天</returns>
        public static DateTime FromSign1Week(string yyyyww)
        {
            if (!yyyyww.StartsWith("w")) throw new ArgumentException("錯誤的Sign");
            yyyyww = yyyyww.Substring(1);
            return FromYyyyWw(yyyyww);
        }

        public static DateTime FromSign1Year(string yyyy)
        {
            if (!yyyy.StartsWith("y")) throw new ArgumentException("錯誤的Sign");
            yyyy = yyyy.Substring(1);
            return FromYyyy(yyyy);
        }
        /// <summary>
        /// Sign Day, e.q. d20201223
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Day(DateTime dt) { return "d" + dt.ToString("yyyyMMdd"); }
        /// <summary>
        /// Sign Day, e.q. d20201223
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Day(DateTime? dt) { return dt.HasValue ? ToSign1Day(dt.Value) : null; }
        /// <summary>
        /// Sign DTime = YyyyMmDdHhIiSs, e.q. s20201223204812
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1DTime(DateTime dt) { return ToSign1Second(dt); }
        /// <summary>
        /// Sign DTime = YyyyMmDdHhIiSs, e.q. s20201223204812
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1DTime(DateTime? dt) { return ToSign1Second(dt); }
        /// <summary>
        /// Sign Quarter, e.q. f202002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1HalfYear(DateTime dt) { var hf = HalfOfYear(dt); return string.Format("f{0}{1:00}", dt.ToString("yyyy"), hf); }
        /// <summary>
        /// Sign Quarter, e.q. f202002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1HalfYear(DateTime? dt) { return dt.HasValue ? ToSign1QuarterYear(dt.Value) : null; }
        /// <summary>
        /// Sign Hour, e.q. h2020122320
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Hour(DateTime dt) { return "h" + dt.ToString("yyyyMMddHH"); }
        /// <summary>
        /// Sign Week, e.q. h2020122320
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Hour(DateTime? dt) { return dt.HasValue ? ToSign1Hour(dt.Value) : null; }
        /// <summary>
        /// Sign Minute, e.q. i202012232048
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Minute(DateTime dt) { return "i" + dt.ToString("yyyyMMddHHmm");/*i 為linux使用的format sign = mInute*/ }
        /// <summary>
        /// Sign Minute, e.q. i202012232048
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Minute(DateTime? dt) { return dt.HasValue ? ToSign1Hour(dt.Value) : null; }
        /// <summary>
        /// Sign Month, e.q. m202012
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Month(DateTime dt) { return "m" + dt.ToString("yyyyMM"); }
        /// <summary>
        /// Sign Month, e.q. m202012
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Month(DateTime? dt) { return dt.HasValue ? ToSign1Month(dt.Value) : null; }
        /// <summary>
        /// Sign Quarter, e.q. q202004
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1QuarterYear(DateTime dt) { var qq = QuarterOfYear(dt); return string.Format("q{0}{1:00}", dt.ToString("yyyy"), qq); }
        /// <summary>
        /// Sign Quarter, e.q. q202004
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1QuarterYear(DateTime? dt) { return dt.HasValue ? ToSign1QuarterYear(dt.Value) : null; }
        /// <summary>
        /// Sign Second, e.q. s20201223204812
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Second(DateTime dt) { return "s" + dt.ToString("yyyyMMddHHmmss"); }
        /// <summary>
        /// Sign Second, e.q. s20201223204812
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Second(DateTime? dt) { return dt.HasValue ? ToSign1Hour(dt.Value) : null; }
        /// <summary>
        /// Sign Week, e.q. w202053
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Week(DateTime dt) { var weekOfYear = CtkTimeUtil.WeekOfYear(dt); return string.Format("w{0}{1:00}", dt.ToString("yyyy"), weekOfYear); }
        /// <summary>
        /// Sign Week, e.q. w202053
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Week(DateTime? dt) { return dt.HasValue ? ToSign1Week(dt.Value) : null; }
        /// <summary>
        /// Sign Year, e.q. y2020
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Year(DateTime dt) { return "y" + dt.ToString("yyyy"); }
        /// <summary>
        /// Sign Year, e.q. y2020
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign1Year(DateTime? dt) { return dt.HasValue ? ToSign1Year(dt.Value) : null; }

        #endregion




        #region Sign DateTime / String : prefix 3 字元


        /// <summary> wek202212 </summary>
        public static DateTime FromSign3Week(string yyyyww)
        {
            if (!yyyyww.StartsWith("wek")) throw new ArgumentException("錯誤的Sign");
            yyyyww = yyyyww.Substring(3);
            return FromYyyyWw(yyyyww);
        }


        /// <summary> mth20220911 </summary>
        public static DateTime FromSign3Day(string yyyymmdd)
        {
            if (!yyyymmdd.StartsWith("day")) throw new ArgumentException("錯誤的Sign");
            yyyymmdd = yyyymmdd.Substring(3);
            return FromYyyyMmDd(yyyymmdd);
        }

        /// <summary> mth20220911 </summary>
        public static DateTime? FromSign3DayTry(string yyyymmdd)
        {
            try
            {
                if (String.IsNullOrEmpty(yyyymmdd)) return null;
                if (!yyyymmdd.StartsWith("day")) throw new ArgumentException("錯誤的Sign");
                yyyymmdd = yyyymmdd.Substring(3);
                return FromYyyyMmDd(yyyymmdd);
            }
            catch (Exception) { return null; }
        }


        /// <summary> mth202209 </summary>
        public static DateTime FromSign3Month(string yyyymm)
        {
            if (!yyyymm.StartsWith("mth")) throw new ArgumentException("錯誤的Sign");
            yyyymm = yyyymm.Substring(3);
            return FromYyyyMm(yyyymm);
        }

        /// <summary> qyr202203 </summary>
        public static DateTime FromSign3Quarter(string yyyyqq)
        {
            if (!yyyyqq.StartsWith("qyr")) throw new ArgumentException("錯誤的Sign");
            yyyyqq = yyyyqq.Substring(3);
            return FromYyyyQq(yyyyqq);
        }

        /// <summary> yr_202203 </summary>
        public static DateTime FromSign3Year(string yyyy)
        {
            if (!yyyy.StartsWith("yr_")) throw new ArgumentException("錯誤的Sign");
            yyyy = yyyy.Substring(3);
            return FromYyyy(yyyy);
        }

        /// <summary> sec20220911201435 </summary>
        public static DateTime FromSign3Second(string yyyymmddhhiiss)
        {
            if (!yyyymmddhhiiss.StartsWith("sec")) throw new ArgumentException("錯誤的Sign");
            yyyymmddhhiiss = yyyymmddhhiiss.Substring(3);
            return FromYyyyMmDdHhIiSs(yyyymmddhhiiss);
        }

        /// <summary> same as Seoncd </summary>
        public static DateTime FromSign3DTime(string dtime) { return FromSign3Second(dtime); }



        /// <summary> day20201223 </summary>
        public static string ToSign3Day(DateTime dt) { return "day" + dt.ToString("yyyyMMdd"); }
        /// <summary>
        /// day20201223
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Day(DateTime? dt) { return dt.HasValue ? ToSign3Day(dt.Value) : null; }
        /// <summary>
        /// sec20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3DTime(DateTime dt) { return ToSign3Second(dt); }
        /// <summary>
        /// sec20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3DTime(DateTime? dt) { return dt.HasValue ? ToSign3DTime(dt.Value) : null; }
        /// <summary>
        /// hyr202002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3HalfYear(DateTime dt) { return string.Format("hyr{0}{1:00}", dt.ToString("yyyy"), HalfOfYear(dt)); }
        /// <summary>
        /// hyr202002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3HalfYear(DateTime? dt) { return dt.HasValue ? ToSign3HalfYear(dt.Value) : null; }
        /// <summary>
        /// hr_2020122321
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Hour(DateTime dt) { return "hr_" + dt.ToString("yyyyMMddHH"); }
        /// <summary>
        /// hr_2020122321
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Hour(DateTime? dt) { return dt.HasValue ? ToSign3Hour(dt.Value) : null; }
        /// <summary>
        /// min202012232102
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Minute(DateTime dt) { return "min" + dt.ToString("yyyyMMddHHmm"); }
        /// <summary>
        /// min202012232102
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Minute(DateTime? dt) { return dt.HasValue ? ToSign3Minute(dt.Value) : null; }
        /// <summary>
        /// mth202012
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Month(DateTime dt) { return "mth" + dt.ToString("yyyyMM");/*mth 是公認的month縮寫, 複數 mths*/ }
        /// <summary>
        /// mth202012
        /// </summary>
        public static string ToSign3Month(DateTime? dt) { return dt.HasValue ? ToSign3Month(dt.Value) : null; }
        /// <summary>
        /// qyr202004
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Quarter(DateTime dt) { return string.Format("qyr{0}{1:00}", dt.ToString("yyyy"), QuarterOfYear(dt)); }
        /// <summary>
        /// qyr202004
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Quarter(DateTime? dt) { return dt.HasValue ? ToSign3Quarter(dt.Value) : null; }
        /// <summary>
        /// sec20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Second(DateTime dt) { return "sec" + dt.ToString("yyyyMMddHHmmss"); }
        /// <summary>
        /// sec20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Second(DateTime? dt) { return dt.HasValue ? ToSign3Second(dt.Value) : null; }
        /// <summary>
        /// wek202053
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Week(DateTime dt) { return string.Format("wek{0}{1:00}", dt.ToString("yyyy"), WeekOfYear(dt)); }
        /// <summary>
        /// wek202053
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Week(DateTime? dt) { return dt.HasValue ? ToSign3Week(dt.Value) : null; }
        /// <summary>
        /// yr_2020
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Yeaer(DateTime dt) { return "yr_" + dt.ToString("yyyy"); }
        /// <summary>
        /// yr_2020
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign3Year(DateTime? dt) { return dt.HasValue ? ToSign3Yeaer(dt.Value) : null; }

        #endregion

        #region Sign DateTime / String : prefix 6 字元

        /// <summary>
        /// day___20201223
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Day(DateTime dt) { return "day___" + dt.ToString("yyyyMMdd"); }
        /// <summary>
        /// day___20201223
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Day(DateTime? dt) { return dt.HasValue ? ToSign6Day(dt.Value) : null; }
        /// <summary>
        /// second20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6DTime(DateTime dt) { return ToSign6Second(dt); }
        /// <summary>
        /// second20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6DTime(DateTime? dt) { return dt.HasValue ? ToSign6DTime(dt.Value) : null; }
        /// <summary>
        /// hfyear202002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6HalfYear(DateTime dt) { return string.Format("hfyear{0}{1:00}", dt.ToString("yyyy"), HalfOfYear(dt)); }
        /// <summary>
        /// hfyear202002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6HalfYear(DateTime? dt) { return dt.HasValue ? ToSign6HalfYear(dt.Value) : null; }
        /// <summary>
        /// hour__2020122321
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Hour(DateTime dt) { return "hour__" + dt.ToString("yyyyMMddHH"); }
        /// <summary>
        /// hour2020122321
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Hour(DateTime? dt) { return dt.HasValue ? ToSign6Hour(dt.Value) : null; }
        /// <summary>
        /// minute202012232102
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Minute(DateTime dt) { return "minute" + dt.ToString("yyyyMMddHHmm"); }
        /// <summary>
        /// minute202012232102
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Minute(DateTime? dt) { return dt.HasValue ? ToSign6Minute(dt.Value) : null; }
        /// <summary>
        /// month_202012
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Month(DateTime dt) { return "month_" + dt.ToString("yyyyMM"); }
        /// <summary>
        /// month_202012
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Month(DateTime? dt) { return dt.HasValue ? ToSign6Month(dt.Value) : null; }
        /// <summary>
        /// qtyear202004
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Quarter(DateTime dt) { return string.Format("qtyear{0}{1:00}", dt.ToString("yyyy"), QuarterOfYear(dt)); }
        /// <summary>
        /// qtyear202004
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Quarter(DateTime? dt) { return dt.HasValue ? ToSign6Quarter(dt.Value) : null; }
        /// <summary>
        /// second2020201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Second(DateTime dt) { return "second" + dt.ToString("yyyyMMddHHmmss"); }
        /// <summary>
        /// second20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Second(DateTime? dt) { return dt.HasValue ? ToSign6Second(dt.Value) : null; }
        /// <summary>
        /// week__202053
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Week(DateTime dt) { return string.Format("week__{0}{1:00}", dt.ToString("yyyy"), WeekOfYear(dt)); }
        /// <summary>
        /// week__202053
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Week(DateTime? dt) { return dt.HasValue ? ToSign6Week(dt.Value) : null; }
        /// <summary>
        /// year__2020
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Yeaer(DateTime dt) { return "year__" + dt.ToString("yyyy"); }
        /// <summary>
        /// year2020
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSign6Year(DateTime? dt) { return dt.HasValue ? ToSign6Yeaer(dt.Value) : null; }

        #endregion


        #region Sign DateTime / String : Full Prefix

        /// <summary>
        /// day20201223
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullDay(DateTime dt) { return "day" + dt.ToString("yyyyMMdd"); }
        /// <summary>
        /// second20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullDTime(DateTime dt) { return ToSignFullSecond(dt); }
        /// <summary>
        /// halfyear202002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullHalfYear(DateTime dt) { return string.Format("halfyear{0}{1:00}", dt.ToString("yyyy"), HalfOfYear(dt)); }
        /// <summary>
        /// hour2020122321
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullHour(DateTime dt) { return "hour" + dt.ToString("yyyyMMddHH"); }
        /// <summary>
        /// minute202012232102
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullMinute(DateTime dt) { return "minute" + dt.ToString("yyyyMMddHHmm"); }
        /// <summary>
        /// month202012
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullMonth(DateTime dt) { return "month" + dt.ToString("yyyyMM"); }
        /// <summary>
        /// quarteryear202004
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullQuarter(DateTime dt) { return string.Format("quarteryear{0}{1:00}", dt.ToString("yyyy"), QuarterOfYear(dt)); }
        /// <summary>
        /// second20201223210251
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullSecond(DateTime dt) { return "second" + dt.ToString("yyyyMMddHHmmss"); }
        /// <summary>
        /// week202053
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullWeek(DateTime dt) { return string.Format("week{0}{1:00}", dt.ToString("yyyy"), WeekOfYear(dt)); }
        /// <summary>
        /// year2020
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSignFullYear(DateTime dt) { return "year" + dt.ToString("yyyy"); }

        #endregion





        #region Normal Compare

        public static int CompareDTime(string dt1, string dt2) { return string.Compare(dt1, dt2); }
        public static int CompareDTime(DateTime dt1, DateTime dt2) { return string.Compare(ToDTime(dt1), ToDTime(dt2)); }
        public static int CompareDTime(DateTime dt1, string dt2) { return string.Compare(ToDTime(dt1), dt2); }
        public static int CompareDTime(string dt1, DateTime dt2) { return string.Compare(dt1, ToDTime(dt2)); }

        public static int CompareYyyy(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyy(dt1), ToYyyy(dt2)); }
        public static int CompareYyyy(DateTime dt1, string dt2) { return string.Compare(ToYyyy(dt1), dt2); }
        public static int CompareYyyy(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyy(dt2)); }

        public static int CompareYyyyHy(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyHy(dt1), ToYyyyHy(dt2)); }
        public static int CompareYyyyHy(DateTime dt1, string dt2) { return string.Compare(ToYyyyHy(dt1), dt2); }
        public static int CompareYyyyHy(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyHy(dt2)); }


        public static int CompareYyyyMm(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyMm(dt1), ToYyyyMm(dt2)); }
        public static int CompareYyyyMm(DateTime? dt1, DateTime? dt2) { return string.Compare(ToYyyyMm(dt1), ToYyyyMm(dt2)); }
        public static int CompareYyyyMm(DateTime dt1, string dt2) { return string.Compare(ToYyyyMm(dt1), dt2); }
        public static int CompareYyyyMm(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyMm(dt2)); }
        public static int CompareYyyyMm(string dt1, string dt2) { return string.Compare(dt1, dt2); }

        public static int CompareYyyyMmDd(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyMmDd(dt1), ToYyyyMmDd(dt2)); }
        public static int CompareYyyyMmDd(DateTime dt1, string dt2) { return string.Compare(ToYyyyMmDd(dt1), dt2); }
        public static int CompareYyyyMmDd(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyMmDd(dt2)); }
        public static int CompareYyyyMmDd(string dt1, string dt2) { return string.Compare(dt1, dt2); }

        public static int CompareYyyyMmDdHh(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyMmDdHh(dt1), ToYyyyMmDdHh(dt2)); }

        public static int CompareYyyyMmDdHh(DateTime dt1, string dt2) { return string.Compare(ToYyyyMmDdHh(dt1), dt2); }

        public static int CompareYyyyMmDdHh(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyMmDdHh(dt2)); }

        public static int CompareYyyyMmDdHhIi(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyMmDdHhIi(dt1), ToYyyyMmDdHhIi(dt2)); }

        public static int CompareYyyyMmDdHhIi(DateTime dt1, string dt2) { return string.Compare(ToYyyyMmDdHhIi(dt1), dt2); }

        public static int CompareYyyyMmDdHhIi(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyMmDdHhIi(dt2)); }

        public static int CompareYyyyMmDdHhIiSs(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyMmDdHhIiSs(dt1), ToYyyyMmDdHhIiSs(dt2)); }

        public static int CompareYyyyMmDdHhIiSs(DateTime dt1, string dt2) { return string.Compare(ToYyyyMmDdHhIiSs(dt1), dt2); }

        public static int CompareYyyyMmDdHhIiSs(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyMmDdHhIiSs(dt2)); }

        public static int CompareYyyyQq(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyQq(dt1), ToYyyyQq(dt2)); }
        public static int CompareYyyyQq(DateTime dt1, string dt2) { return string.Compare(ToYyyyQq(dt1), dt2); }
        public static int CompareYyyyQq(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyQq(dt2)); }


        public static int CompareYyyyWw(string dt1, string dt2) { return string.Compare(dt1, dt2); }
        public static int CompareYyyyWw(DateTime dt1, DateTime dt2) { return string.Compare(ToYyyyWw(dt1), ToYyyyWw(dt2)); }
        public static int CompareYyyyWw(DateTime? dt1, DateTime? dt2) { return string.Compare(ToYyyyWw(dt1), ToYyyyWw(dt2)); }
        public static int CompareYyyyWw(DateTime dt1, string dt2) { return string.Compare(ToYyyyWw(dt1), dt2); }
        public static int CompareYyyyWw(string dt1, DateTime dt2) { return string.Compare(dt1, ToYyyyWw(dt2)); }


        #endregion

        #region Compare Sign3


        public static int CompareSign3DTime(string dt1, DateTime dt2) { return string.Compare(dt1, ToSign3DTime(dt2)); }


        #endregion



        #region Linux Timestamp

        public static DateTime ToDateTimeFromMilliTimestamp(double timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(timestamp);
        }

        public static DateTime ToDateTimeFromTimestamp(double timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp);
        }

        public static DateTime ToLocalDateTimeFromTimestamp(double timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp).ToLocalTime();
        }


        public static Int64 ToMilliTimestamp()
        {
            return ToMilliTimestamp(DateTime.Now);
        }
        public static Int64 ToMilliTimestamp(DateTime dt)
        {
            return (Int64)(dt - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static double ToTimestamp()
        {
            return ToTimestamp(DateTime.Now);
        }
        public static double ToTimestamp(DateTime dt)
        {
            return (dt - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        public static Int64 ToUtcMilliTimestamp(DateTime dt)
        {
            return (Int64)(dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static double ToUtcTimestamp()
        {
            return ToUtcTimestamp(DateTime.Now);
        }
        public static double ToUtcTimestamp(DateTime dt)
        {
            return (dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        #endregion


        #region ROC DateTime

        public static DateTime FromRocDateToAd(DateTime dt) { return dt.AddYears(YearDiffBetweenRocAndAd); }

        public static DateTime FromRocDateToAdSpliter(string s, char spliter)
        {
            var dt = new DateTime();
            if (!FromRocDateToAdSpliterTry(s, spliter, ref dt)) throw new CtkException("Cannot convert to DateTime");
            return dt;
        }

        public static bool FromRocDateToAdSpliterTry(string s, char spliter, ref DateTime dt)
        {
            var nums = s.Split(spliter);
            if (nums.Length != 3) return false;
            var yyy = ToAdYearFromRoc(Convert.ToInt32(nums[0]));//會遇到潤2月, 因此先轉
            var mm = Convert.ToInt32(nums[1]);
            var dd = Convert.ToInt32(nums[2]);
            dt = new DateTime(yyy, mm, dd);
            return true;
        }
        /// <summary>
        /// 不建議使用, 小於民國100年的, 會被視為19xx年, e.q. 88/01/15 會變成 1988/01/15
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Obsolete("A incorrect convertion when year<100")]
        public static bool FromRocDateToAdTry(string s, out DateTime result, string format = "yyy.MM.dd")
        {
            if (!DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return false;
            result = CtkTimeUtil.FromRocDateToAd(result);
            return true;
        }

        public static int ToAdYearFromRoc(int year) { return year + YearDiffBetweenRocAndAd; }
        public static DateTime ToRocDateFromAd(DateTime dt) { return dt.AddYears(-YearDiffBetweenRocAndAd); }
        public static int ToRocYearFromAd(int year) { return year - YearDiffBetweenRocAndAd; }
        #endregion



    }
}
