using SurveyApi.DTOs;
using SurveyApi.Entities;
using SurveyApi.Repositories;

namespace SurveyApi.Services
{
    public class SurveyService(
        IQuestionRepository questionRepository,
        IInterviewRepository interviewRepository,
        IResultRepository resultRepository) : ISurveyService
    {
        public async Task<QuestionDto?> GetQuestionAsync(int questionId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);

            if (question == null) 
                return null;

            var answerDtos = question.Answers
                .Select(answer => new AnswerDto { Id = answer.Id, Text = answer.Text })
                .ToList();

            return new QuestionDto
            { 
                Id = question.Id,
                Text = question.Text,
                Answers = answerDtos
            };
        }

        public async Task<QuestionAndInterviewDto> StartSurveyAsync(StartSurveyDto startSurveyDto)
        {
            var interview = new Interview
            {
                SurveyId = startSurveyDto.SurveyId,
                UserId = startSurveyDto.UserId,
                DateStarted = DateTime.UtcNow,
            };

            return new QuestionAndInterviewDto
            { 
                InterviewId = await interviewRepository.AddInterviewAsync(interview),
                QuestionId = await questionRepository.GetFirstQuestionIdBySurveyIdAsync(startSurveyDto.SurveyId)
            }; 
        }

        public async Task<QuestionIdDto> SaveAnswerAsync(SaveAnswerDto saveAnswerDto)
        {
            var result = new Result
            {
                InterviewId = saveAnswerDto.InterviewId,
                QuestionId = saveAnswerDto.QuestionId,
                AnswerId = saveAnswerDto.AnswerId
            };

            await resultRepository.AddResultAsync(result);

            return new QuestionIdDto
            {
                QuestionId = await questionRepository.GetNextQuestionIdAsync(saveAnswerDto.QuestionId)
            };
        }
    }
}
