using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Srt
{
  public class TextFileRW
  {
    #region fields
    protected string _fileName = "";
    protected FileMode _fileMode = FileMode.Open;
    protected FileStream? _fileStream = null;
    protected StreamReader? _streamReader = null;
    protected StreamWriter? _streamWriter = null;
    #endregion

    #region constructor
    public TextFileRW(string fileName, FileMode fileMode)
    {
      this._fileName = fileName;
      this._fileMode = fileMode;
    }
    #endregion

    #region public methods
    public bool Open()
    {
      if (this._fileStream != null)
      {
        this.Close();
      }
      if (this._fileMode != FileMode.Open && this._fileMode != FileMode.Create)
      {
        Console.WriteLine("*** Dozwolone tryby tylko Open lub Create");
        return false;
      }
      try
      {
        this._fileStream = new FileStream(this._fileName, this._fileMode);
        if (this._fileMode == FileMode.Open) 
        {
          this._streamWriter = null;
          this._streamReader = new StreamReader(this._fileStream);
        }
        if (this._fileMode == FileMode.Create)
        {
          this._streamReader = null;
          this._streamWriter = new StreamWriter(this._fileStream);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
      return true;
    }

    public void Close() 
    {
      if (this._fileStream == null)
        return;
      if (this._streamWriter != null)
      {
        this._streamWriter.Flush();
      }
      this._fileStream.Close();
      this._fileStream.Dispose();
      this._fileStream = null;
    }

    public string? ReadLine()
    {
      if (this._streamReader == null)
      {
        Console.WriteLine("*** Plik nie został otwarty do odczytu");
        return null;
      }
      return this._streamReader.ReadLine();
    }

    public bool WriteLine(string line)
    {
      if (this._streamWriter == null)
      {
        Console.WriteLine("*** Plik nie został otwarty do zapisu");
        return false;
      }
      this._streamWriter.Write(line + "\n");
      return true;
    }
    #endregion
  }
}
