using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public class QuestionRepository(AppDbContext context) : IQuestionRepository
    {
        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            return await context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }
    }
}
