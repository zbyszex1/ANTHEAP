using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srt
{
  internal class Processing
  {
    public static void Loop(string fileName)
    {
      TextFileRW source = null;
      TextFileRW target0 = null;
      TextFileRW target1 = null;
      int number0 = 1;
      int number1 = 1;
      string line;
      try
      {
        string _e = Path.GetExtension(fileName);
        string _n = Path.GetFileNameWithoutExtension(fileName);
        string _d = Path.GetDirectoryName(fileName);
        string name0 = Path.Combine(_d, String.Format("{0}0{1}", _n, _e));
        string name1 = Path.Combine(_d, String.Format("{0}1{1}", _n, _e));
        source = new TextFileRW(fileName, FileMode.Open);
        target0 = new TextFileRW(name0, FileMode.Create);
        target1 = new TextFileRW(name1, FileMode.Create);
        if (!source.Open())
        {
          throw new Exception("Błąd otwarcia pliku: '" + fileName + "'!");
        }
        if (!target0.Open())
        {
          throw new Exception("Bład utworzenia pliku: '" + name0 + "'!");
        }
        if (!target1.Open())
        {
          throw new Exception("Bład utworzenia pliku: '" + name1 + "'!");
        }
        Console.WriteLine("Rozpoczęcie analizy pliku");
        StringBuilder sb = new StringBuilder();
        int cnt = 1;
        int sub_cnt = 0;
        do
        {
          line = source.ReadLine();
          cnt++;
          if ((line == null || line.Length == 0) && sub_cnt > 0)
          {
            string subline = sb.ToString();
            Subtitle subtitle = new Subtitle();
            SubData subdata = subtitle.Parse(subline);
            if (subdata == null)
            {
              throw new Exception(String.Format("Bład parsowania pliku. Wiersz {0}", cnt));
            }
            bool fullSecond = subtitle.Shift();
            if (fullSecond)
            {
              subtitle.SetNumber(number0++);
              target0.WriteLine(subtitle.Print());
            }
            else
            {
              subtitle.SetNumber(number1++);
              target1.WriteLine(subtitle.Print());
            }
            Console.Write(subtitle.GetNumber().ToString() + "\r");
            sb = new StringBuilder();
            sub_cnt = 0;
          }
          else
          {
            if ((line != null && line.Length > 0) || sub_cnt > 0)
            {
              sb.AppendLine(line);
              sub_cnt++;
            }
          }
        }
        while (line != null);
        Console.WriteLine("\nZakończene analizy pliku");
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
      finally
      {
        source?.Close();
        target0?.Close();
        target1?.Close();
      }
    }
  }
}
