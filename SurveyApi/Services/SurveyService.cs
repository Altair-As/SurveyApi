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

        public async Task<QuestionAndInterviewDto> StartSurveyAsync(int surveyId, int userId)
        {
            var interview = new Interview
            {
                SurveyId = surveyId,
                UserId = userId,
                DateStarted = DateTime.UtcNow,
            };

            return new QuestionAndInterviewDto
            { 
                InterviewId = await interviewRepository.AddInterviewAsync(interview),
                QuestionId = await questionRepository.GetFirstQuestionIdBySurveyIdAsync(surveyId)
            }; 
        }

        public async Task<QuestionIdDto> SaveAnswerAsync(int interviewId, int questionId, int answerId)
        {
            var result = new Result
            {
                InterviewId = interviewId,
                QuestionId = questionId,
                AnswerId = answerId
            };

            await resultRepository.AddResultAsync(result);

            return new QuestionIdDto 
            { 
                QuestionId = await questionRepository.GetNextQuestionIdAsync(questionId) 
            };
        }
    }
}
