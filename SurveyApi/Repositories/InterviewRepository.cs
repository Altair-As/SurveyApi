using Microsoft.EntityFrameworkCore;
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

        public async Task FinishInterviewAsync(int interviewId)
        {
            var interview = await context.Interviews.FindAsync(interviewId)
                ?? throw new InvalidOperationException($"Invalid interview id: {interviewId}");

            interview.DateStarted = DateTime.SpecifyKind(interview.DateStarted, DateTimeKind.Utc);
            interview.DateCompleted = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            context.Interviews.Update(interview);
            await context.SaveChangesAsync();
        }
    }
}
