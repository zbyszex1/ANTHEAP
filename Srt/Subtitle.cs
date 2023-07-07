using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Srt
{
  public class Subtitle
  {

    #region fields
    protected SubData subData;
    protected int _cnt = 0;
    protected string _number;
    protected string _range;
    protected string _subtitle;
    protected TimeSpan _span;
    const string _timeFormat = "HH:mm:ss,fff";
    const string _matchNumber = @"\d+\n";
    const string _matchTime = @"((2[0-3])|([01][0-9]))(:[0-5][0-9]){2},\d{3}";
    const string _matchTimeRange = _matchTime + @"\s+\-\->\s+" + _matchTime + @"\n";
    const string _matchEnd =@"\n";
    #endregion

    #region constructors
    public Subtitle()
    {
      subData = new SubData();
      subData._number = 1;
      subData._subtitle = "";
      _span = new TimeSpan(0, 0, 0, 5, 880);
    }
    public Subtitle(SubData subData)
    {
      this.subData = subData;
      _span = new TimeSpan(0, 0, 0, 5, 880);
    }
    #endregion

    #region protected methods
    protected bool ParseNumber()
    {
      Regex rN = new Regex(_matchNumber);
      Match mN = rN.Match(_subtitle);
      if (!mN.Success)
        return false;
      _number = mN.Value;
      bool parsed = int.TryParse(_number, out subData._number);
      return parsed;
    }

    protected bool ParseTimes()
    {
      Regex rR = new Regex(_matchTimeRange);
      Match mR = rR.Match(_subtitle);
      if (!mR.Success)
        return false;
      _range = mR.Value;
      var mBE = Regex.Matches(_range, _matchTime);
      _cnt = 0;
      foreach (Match m in mBE)
      {
        if (_cnt == 0)
          subData._begin = DateTime.ParseExact(m.Value, _timeFormat, null);
        else
          subData._end = DateTime.ParseExact(m.Value, _timeFormat, null);
        if (_cnt > 0)
          break;
        _cnt++;
      }
      return true;
    }

    protected bool ParseSubtitle() 
    {
      string subsub = _subtitle.Substring(_number.Length + _range.Length);
      Regex rS = new Regex(_matchEnd);
      Match mS = rS.Match(subsub);
      if (!mS.Success)
        return false;
      subData._subtitle = subsub.Substring(0, subsub.Length - mS.Value.Length);
      return true;
    }
    #endregion

    #region public methods
    public SubData Parse(string subtitle ) 
    {
      if (subtitle == null || subtitle.Length < 32)
        return null;
      _subtitle = String.Copy(subtitle).ReplaceLineEndings("\n");
      subData = new SubData();
      if (subtitle == null)
        return null;
      if (!ParseNumber())
        return null;
      if (!ParseTimes())
        return null;
      if (!ParseSubtitle())
        return null;
      return subData;
    }
    public bool Shift()
    {
      subData._begin += _span;
      subData._end += _span;
      return subData._begin.Millisecond == 0;
    }
    public int GetNumber()
    {
      return subData._number;
    }
    public void SetNumber(int number)
    {
      subData._number = number;
    }
    public string Print()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendFormat("{0}{1}\n", subData._number > 1 ? "\n" : "", subData._number);
      sb.AppendFormat("{0} --> {1}\n", SubData.PrintTime(subData._begin), SubData.PrintTime(subData._end));
      sb.AppendFormat("{0}", subData._subtitle);
      return sb.ToString();
    }
    #endregion
  }
}
