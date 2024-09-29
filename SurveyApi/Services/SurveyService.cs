using SurveyApi.DTOs;
using SurveyApi.Entities;
using SurveyApi.Repositories;

namespace SurveyApi.Services
{
    public class SurveyService(
        IQuestionRepository questionRepository,
        IInterviewRepository interviewRepository,
        IResultRepository resultRepository) : ISurveyService
    {
        public async Task<QuestionDto?> GetQuestionAsync(int questionId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);

            if (question == null) 
                return null;

            var answerDtos = question.Answers
                .Select(answer => new AnswerDto { Id = answer.Id, Text = answer.Text })
                .ToList();

            return new QuestionDto
            { 
                Id = question.Id,
                Text = question.Text,
                Answers = answerDtos
            };
        }

        public async Task<int> StartInterviewAsync(StartSurveyDto startSurveyDto)
        {
            var interview = new Interview
            {
                SurveyId = startSurveyDto.SurveyId,
                UserId = startSurveyDto.UserId,
                DateStarted = DateTime.UtcNow,
            };

            var interviewId = await interviewRepository.AddInterviewAsync(interview);

            return interviewId;
        }

        public async Task<int> GetFirstQuestionAsync(int surveyId)
        {
            var firstQuestionId = await questionRepository.GetFirstQuestionIdBySurveyIdAsync(surveyId);

            return firstQuestionId;
        }

        public async Task SaveAnswerAsync(SaveAnswerDto saveAnswerDto)
        {
            var result = new Result
            {
                InterviewId = saveAnswerDto.InterviewId,
                QuestionId = saveAnswerDto.QuestionId,
                AnswerId = saveAnswerDto.AnswerId
            };

            await resultRepository.AddResultAsync(result);
        }

        public async Task<int> GetNextQuestionIdAsync(int questionId)
        {
            var nextQuestionId = await questionRepository.GetNextQuestionIdAsync(questionId);

            return nextQuestionId;
        }

        public async Task FinishInterviewAsync(int interviewId)
        {
            await interviewRepository.FinishInterviewAsync(interviewId);
        }
    }
}
