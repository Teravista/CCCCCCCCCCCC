using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
namespace DI.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private const string Expected = "Hello, World!";
        [TestMethod]
        [DataRow(true)]
        //[DataRow(false)]
        public void TestMainMethodShouldReturnExpectedString(bool isTrue)
        {
            if (isTrue)
            {
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    HelloWorld hw = new();
                    hw.WriteHelloWorldLine();
                    var result = sw.ToString().Trim();
                    Assert.AreEqual(Expected, result);
                }
            }
            else
            {
                //...
            }
        }
    }
}
