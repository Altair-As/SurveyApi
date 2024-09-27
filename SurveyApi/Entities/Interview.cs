namespace SurveyApi.Entities
{
    public class Interview
    {
        public int Id { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Result> Results { get; set; } = new List<Result>();

    }
}
