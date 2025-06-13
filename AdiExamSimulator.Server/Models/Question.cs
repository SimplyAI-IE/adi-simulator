namespace AdiExamSimulator.Server.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public string CorrectOption { get; set; } = null!;
        public string Category { get; set; } = null!;
        public double Weighting { get; set; }
        public string? Explanation { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
