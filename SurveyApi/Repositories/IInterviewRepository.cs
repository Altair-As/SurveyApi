using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public interface IInterviewRepository
    {
        Task<int> AddInterviewAsync(Interview interview);
        Task FinishInterviewAsync(int interviewId);
    }
}
