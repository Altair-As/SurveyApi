namespace SurveyApi.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public required string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
