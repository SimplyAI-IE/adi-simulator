namespace AdiExamSimulator.Server.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public int UserId { get; set; }
        public DateTime ExamDate { get; set; } = DateTime.UtcNow;
        public int TotalQuestions { get; set; }
        public int CorrectCount { get; set; }
        public double Score { get; set; }
        public string BreakdownJSON { get; set; } = null!;

        // Navigation property
        public User? User { get; set; }
    }
}
