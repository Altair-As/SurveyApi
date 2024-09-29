using SurveyApi.DTOs;

namespace SurveyApi.Services
{
    public interface ISurveyService
    {
        Task<QuestionDto?> GetQuestionAsync(int questionId);
        Task<QuestionAndInterviewDto> StartSurveyAsync(int surveyId, int userId);
        Task<QuestionIdDto> SaveAnswerAsync(int interviewId, int questionId, int answerId);
    }
}
