using Microsoft.AspNetCore.Authorization;
using AdiExamSimulator.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdiExamSimulator.Server.Data;
using System.Security.Claims;

namespace AdiExamSimulator.Server.Controllers
{
    [ApiController]
    [Route("results")]
    [Authorize]
    public class ResultsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResultsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitResult([FromBody] ResultSubmission submission)
        {
            var userId = int.Parse(User.FindFirst("UserID")!.Value);

            var result = new Result
            {
                UserId = userId,
                TotalQuestions = submission.TotalQuestions,
                CorrectCount = submission.CorrectCount,
                Score = submission.Score,
                BreakdownJSON = submission.BreakdownJSON,
                ExamDate = DateTime.UtcNow
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var userId = int.Parse(User.FindFirst("UserID")!.Value);
            var results = await _context.Results
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.ExamDate)
                .ToListAsync();

            return Ok(results);
        }
    }
}
