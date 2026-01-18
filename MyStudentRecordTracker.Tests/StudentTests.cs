using Xunit;
using HelloWorld;

namespace HelloWorld.Tests
{
    public class StudentTests
    {
        [Fact]
        public void ScoreMessage_UsesClampedScore()
        {
            var student = new Student("Priya", 120);

            string message = student.ScoreMessage();

            Assert.Equal("Priya: Score accepted: 100", message);
        }
    }
}