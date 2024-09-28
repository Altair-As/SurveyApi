using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Entities;

namespace SurveyApi.Repositories
{
    public class QuestionRepository(AppDbContext context) : IQuestionRepository
    {
        public async Task<int> GetNextQuestionIdAsync(int questionId)
        {
            var currentQuestion = await context.Questions
                .Where(q => q.Id == questionId)
                .Select(q => new { q.SurveyId, q.QuesionOrder })
                .FirstOrDefaultAsync()
                ?? throw new InvalidOperationException($"Unable to find question with id: {questionId}");

            var nextQuestionId = await context.Questions
                .Where (q => q.SurveyId == currentQuestion.SurveyId && q.QuesionOrder > currentQuestion.QuesionOrder)
                .OrderBy(q => q.QuesionOrder)
                .Select(q => q.Id)
                .FirstOrDefaultAsync();

            return nextQuestionId == 0 ? -1 : nextQuestionId;
        }

        public async Task<int> GetFirstQuestionIdBySurveyIdAsync(int surveyId)
        {
            var firstQuestionId = await context.Questions
                .Where(q => q.SurveyId == surveyId)
                .OrderBy(q => q.QuesionOrder)
                .Select(q => q.Id)
                .FirstOrDefaultAsync();

            return firstQuestionId == 0 ? -1 : firstQuestionId;
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            return await context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }
    }
}
