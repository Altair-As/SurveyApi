using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public interface IQuestionRepository
    {
        Task<int> GetNextQuestionId(int questionId);
        Task<int> GetFirstQuestionIdBySurveyId(int surveyId);
        Task<Question?> GetQuestionByIdAsync(int questionId);
    }
}
