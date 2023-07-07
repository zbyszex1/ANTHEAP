namespace Srt.Test
{
  public class SubtitleTest
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SingleSubtitleParseTest()
    {
      string input = "209\n00:25:19,880 --> 00:25:21,711\nMat�j z Bohdance.\n\n";
      SubData expected = new SubData();
      expected._number = 209;
      expected._begin = SubData.SetTime(0, 25, 19, 880);
      expected._end = SubData.SetTime(0, 25, 21, 711);
      expected._subtitle = "Mat�j z Bohdance.\n";
      Subtitle subtitle = new Subtitle();

      SubData result = subtitle.Parse(input);

      Assert.IsTrue(SubData.Equals(result, expected));
    }

    [Test]
    public void DoubleSubtitleParseTest()
    {
      string input = "1227\n01:57:06,400 --> 01:57:08,152\nChcete, aby m� sebral\n�tan �i Vlk?\n\n";
      SubData expected = new SubData();
      expected._number = 1227;
      expected._begin = SubData.SetTime(1, 57, 6, 400);
      expected._end = SubData.SetTime(1, 57, 8, 152);
      expected._subtitle = "Chcete, aby m� sebral\n�tan �i Vlk?\n";
      Subtitle subtitle = new Subtitle();

      SubData result = subtitle.Parse(input);

      Assert.IsTrue(SubData.Equals(result, expected));
    }

    [Test]
    public void PrintTest()
    {
      string expected = "1015\n01:39:36,880 --> 01:39:40,350\nAle p�edt�m\nzkus�me je�t� cestu sm�ru.\n";

      SubData subData = new SubData();
      subData._number = 1015;
      subData._begin = SubData.SetTime(1, 39, 36, 880);
      subData._end = SubData.SetTime(1, 39, 40, 350);
      subData._subtitle = "Ale p�edt�m\nzkus�me je�t� cestu sm�ru.";
      Subtitle subtitle = new Subtitle(subData);
      string result = subtitle.Print();

      Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ShiftPositiveTest()
    {
      string input = "1\n01:57:06,120 --> 01:57:08,152\nX\n\n";
      Subtitle subtitle = new Subtitle();
      subtitle.Parse(input);

      bool result = subtitle.Shift();

      Assert.IsTrue(result);
    }

    [Test]
    public void ShiftNegativeTest()
    {
      string input = "1\n01:57:06,121 --> 01:57:08,152\nX\n\n";
      Subtitle subtitle = new Subtitle();
      subtitle.Parse(input);

      bool result = subtitle.Shift();

      Assert.IsFalse(result);
    }
  }
}