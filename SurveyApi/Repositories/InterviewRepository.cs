using SurveyApi.Data;
using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public class InterviewRepository(AppDbContext context) : IInterviewRepository
    {
        public async Task<int> AddInterviewAsync(Interview interview)
        {
            await context.Interviews.AddAsync(interview);
            await context.SaveChangesAsync();

            return interview.Id;
        }
    }
}
