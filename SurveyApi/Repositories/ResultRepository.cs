using SurveyApi.Data;
using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public class ResultRepository(AppDbContext context) : IResultRepository
    {
        public async Task AddResultAsync(Result result)
        {
            await context.Results.AddAsync(result);
            await context.SaveChangesAsync();
        }
    }
}
