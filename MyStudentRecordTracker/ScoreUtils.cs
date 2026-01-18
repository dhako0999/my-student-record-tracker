namespace HelloWorld
{
    public static class ScoreUtils
    {
        public static int ClampScore(int score)
        {
            if (score > 100) return 100;
            if (score < 0) return 0;
            return score;
        }

        public static char LetterGrade(int score)
        {
            int clampedScore = ClampScore(score);

            if (clampedScore >= 90) return 'A';
            if (clampedScore >= 80) return 'B';
            if (clampedScore >= 70) return 'C';
            if (clampedScore >= 60) return 'D';
            return 'F';
        }
    }
}