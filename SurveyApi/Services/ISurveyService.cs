using SurveyApi.DTOs;

namespace SurveyApi.Services
{
    public interface ISurveyService
    {
        Task<QuestionDto?> GetQuestionAsync(int questionId);
        Task<int> StartInterviewAsync(StartSurveyDto startSurveyDto);
        Task<int> GetFirstQuestionAsync(int surveyId);
        Task SaveAnswerAsync(SaveAnswerDto saveAnswerDto);
        Task FinishInterviewAsync(int interviewId);
        Task<int> GetNextQuestionIdAsync(int questionId);
    }
}
