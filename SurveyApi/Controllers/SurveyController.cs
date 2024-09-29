using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApi.DTOs;
using SurveyApi.Services;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController(ISurveyService surveyService) : ControllerBase
    {
        [HttpGet("{questionId}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int questionId)
        {
            var question = await surveyService.GetQuestionAsync(questionId);

            if (question == null) 
                return NotFound();

            return Ok(question);
        }

        [HttpPost("save-answer")]
        public async Task<ActionResult<QuestionIdDto>> SaveAswer([FromBody] SaveAnswerDto saveAnswerDto)
        {
            var question = await surveyService.SaveAnswerAsync(saveAnswerDto);

            if (question.QuestionId == -1)
                return NoContent();

            return Ok(question);
        }

        [HttpPost("start-survey")]
        public async Task<ActionResult<QuestionAndInterviewDto>> StartSurvey([FromBody] StartSurveyDto startSurveyDto)
        {
            var question = await surveyService.StartSurveyAsync(startSurveyDto);

            if (question.QuestionId == -1)
                return NoContent();

            return Ok(question);
        }
    }
}
