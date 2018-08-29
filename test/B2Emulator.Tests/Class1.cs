using Xunit;

namespace B2Emulator.Tests
{
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Startup.Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Startup.Add(2, 3));
        }
    }
}
