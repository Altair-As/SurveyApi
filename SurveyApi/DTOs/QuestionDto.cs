namespace SurveyApi.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
    }
}
