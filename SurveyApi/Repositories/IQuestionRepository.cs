using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public interface IQuestionRepository
    {
        Task<int> GetNextQuestionIdAsync(int questionId);
        Task<int> GetFirstQuestionIdBySurveyIdAsync(int surveyId);
        Task<Question?> GetQuestionByIdAsync(int questionId);
    }
}
