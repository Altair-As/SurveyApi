namespace SurveyApi.DTOs
{
    public class SaveAnswerDto
    {
        public int InterviewId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
