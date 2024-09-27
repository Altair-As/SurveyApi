namespace SurveyApi.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public int QuesionOrder { get; set; }

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    }
}
