using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public interface IResultRepository
    {
        Task AddResultAsync(Result result);
    }
}
