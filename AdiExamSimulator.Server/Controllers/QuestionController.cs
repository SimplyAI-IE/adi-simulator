using Microsoft.AspNetCore.Mvc;
using AdiExamSimulator.Server.Models;
using AdiExamSimulator.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Security.Claims;

namespace AdiExamSimulator.Server.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        [AdminAuthorize]
        public async Task<IActionResult> AddQuestions([FromBody] List<Question> questions)
        {
            _context.Questions.AddRange(questions);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("enable-disable/{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> ToggleQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();

            question.IsEnabled = !question.IsEnabled;
            await _context.SaveChangesAsync();
            return Ok(question.IsEnabled);
        }

        [HttpDelete("delete/{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("random")]
[Authorize]
public async Task<IActionResult> GetRandomExam()
{
    var enabledQuestions = await _context.Questions.Where(q => q.IsEnabled).ToListAsync();

    if (!enabledQuestions.Any())
        return BadRequest("No enabled questions available.");

    // Create weighted pools by category
    var categoryWeights = new Dictionary<string, double>
    {
        { "RulesOfTheRoad", 0.25 },
        { "DrivingTechniques", 0.15 },
        { "TeachingTheory", 0.20 },
        { "Psychology", 0.10 },
        { "Legal", 0.15 },
        { "EDT", 0.15 }
    };

    // Build final exam set
    var examQuestions = new List<Question>();

    foreach (var category in categoryWeights.Keys)
    {
        var categoryQuestions = enabledQuestions.Where(q => q.Category == category).ToList();
        if (categoryQuestions.Count == 0) continue;

        int targetCount = (int)Math.Round(categoryWeights[category] * 100);
        var randomSample = categoryQuestions.OrderBy(x => Guid.NewGuid()).Take(targetCount);
        examQuestions.AddRange(randomSample);
    }

    // Ensure exactly 100 questions (filler logic if some categories lacked enough questions)
    while (examQuestions.Count < 100)
    {
        var filler = enabledQuestions
            .Where(q => !examQuestions.Contains(q))
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefault();

        if (filler != null)
            examQuestions.Add(filler);
        else
            break;
    }

    examQuestions = examQuestions.Take(100).ToList();

    return Ok(examQuestions);
}

    }
}
