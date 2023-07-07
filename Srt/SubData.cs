using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srt
{
  public class SubData
  {
    #region fields
    public int _number;
    public DateTime _begin;
    public DateTime _end;
    public string? _subtitle;
    #endregion

    #region public static methods
    public static bool Equals(SubData x, SubData y)
    {
      if (x == null && y == null) { return true; }
      else if (x == null || y == null) { return false; }
      else if (object.ReferenceEquals(x, y)) { return true; }

      return x._number == y._number
          && x._begin == y._begin
          && x._end == y._end
          && x._subtitle == y._subtitle;
    }
    public static DateTime SetTime(int hour, int minute, int second, int millisecond) 
    {
      DateTime now = DateTime.Now;
      return new DateTime(now.Year, now.Month, now.Day, hour, minute, second, millisecond);
    }
    public static string PrintTime(DateTime time)
    {
      return time.ToString("HH:mm:ss,fff");
    }
    public static string PrintTime(DateTime time, TimeSpan span) 
    {
      return PrintTime(time + span);
    }
    #endregion
  }
}
