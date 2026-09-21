using Xunit;
using EAS_Project;

namespace Gravity_Blockers.Tests
{
    public class PlayerSetupTests
    {
        [Fact]
        public void GetSinglePlayer_ReturnsMike()
        {
            var input = new StringReader("Mike");
            Console.SetIn(input);

            string result = PlayerSetup.GetSinglePlayer("A");

            Assert.Equal("Mike", result);
        }
    }
}
