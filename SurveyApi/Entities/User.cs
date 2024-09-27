namespace SurveyApi.Entities
{
    public class User
    {
        public int Id { get; set; }

        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }
}
