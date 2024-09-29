using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApi.DTOs;
using SurveyApi.Entities;
using SurveyApi.Services;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController(ISurveyService surveyService) : ControllerBase
    {
        [HttpGet("get-question")]
        public async Task<ActionResult<QuestionDto>> GetQuestion([FromQuery] int questionId)
        {
            var question = await surveyService.GetQuestionAsync(questionId);

            if (question == null) 
                return NotFound();

            return Ok(question);
        }

        [HttpPost("save-answer")]
        public async Task<ActionResult<QuestionIdDto>> SaveAswer([FromBody] SaveAnswerDto saveAnswerDto)
        {
            await surveyService.SaveAnswerAsync(saveAnswerDto);
            var questionId = await surveyService.GetNextQuestionIdAsync(saveAnswerDto.QuestionId);

            if (questionId == -1)
            {
                await surveyService.FinishInterviewAsync(saveAnswerDto.InterviewId);
                return NoContent();
            }

            return Ok(new QuestionIdDto
            {
                QuestionId = questionId
            });
        }

        [HttpPost("start-survey")]
        public async Task<ActionResult<QuestionAndInterviewDto>> StartSurvey([FromBody] StartSurveyDto startSurveyDto)
        {
            var interviewId = await surveyService.StartInterviewAsync(startSurveyDto);
            var questionId = await surveyService.GetFirstQuestionAsync(startSurveyDto.SurveyId);

            if (questionId == -1)
                return NoContent();

            return Ok(new QuestionAndInterviewDto
            {
                InterviewId = interviewId,
                QuestionId = questionId
            });
        }
    }
}
