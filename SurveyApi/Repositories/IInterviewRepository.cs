using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public interface IInterviewRepository
    {
        Task AddInterviewAsync(Interview interview);
    }
}
