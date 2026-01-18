using Xunit;
using HelloWorld;

namespace HelloWorld.Tests
{
    public class ScoreUtilsTests
    {
        [Theory]
        [InlineData(120, 100)]
        [InlineData(100, 100)]
        [InlineData(50, 50)]
        [InlineData(0, 0)]
        [InlineData(-10, 0)]
        public void ClampScore_WorksCorrectly(int input, int expected)
        {
            int result = ScoreUtils.ClampScore(input);
            Assert.Equal(expected, result);
        }
    }
}