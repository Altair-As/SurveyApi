using SurveyApi.DTOs;

namespace SurveyApi.Services
{
    public interface ISurveyService
    {
        Task<QuestionDto?> GetQuestionAsync(int questionId);
        Task<QuestionAndInterviewDto> StartSurveyAsync(StartSurveyDto startSurveyDto);
        Task<QuestionIdDto> SaveAnswerAsync(SaveAnswerDto saveAnswerDto);
    }
}
