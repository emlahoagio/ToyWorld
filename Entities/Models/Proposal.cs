namespace Entities.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rule { get; set; }
        public string Slogan { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
