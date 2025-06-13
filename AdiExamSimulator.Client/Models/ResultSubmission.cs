namespace AdiExamSimulator.Client.Models
{
    public class ResultSubmission
    {
        public int TotalQuestions { get; set; }
        public int CorrectCount { get; set; }
        public double Score { get; set; }
        public string BreakdownJSON { get; set; } = string.Empty;
    }
}
