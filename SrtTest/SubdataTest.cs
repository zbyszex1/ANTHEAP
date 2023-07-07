namespace Srt.Test
{
  public class SubdataeTest
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PrintTimeTest()
    {
      DateTime dt = SubData.SetTime(0, 25, 19, 880);
      string expected = "00:25:19,880";

      string result = SubData.PrintTime(dt);

      Assert.That(result, Is.EqualTo(expected));
    }

  }
}