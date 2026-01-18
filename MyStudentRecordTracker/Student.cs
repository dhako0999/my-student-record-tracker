namespace HelloWorld
{
    public class Student
    {
        public string Name { get; }
        public int Score { get; }

        public Student(string name, int score)
        {
            Name = name;
            Score = score;

        }

        public string GetMessage()
        {
            int clampedScore = ScoreUtils.ClampScore(Score);

            return $"{Name}: Score Accepted: {clampedScore}";
        }

        public string ScoreMessage()
        {
            int clampedScore = ScoreUtils.ClampScore(Score);
            return $"{Name}: Score accepted: {clampedScore}";

        }
    
    }
}