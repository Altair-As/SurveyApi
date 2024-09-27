using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question?> GetQuestionByIdAsync(int questionId);
    }
}
