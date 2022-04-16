namespace Entities.RequestFeatures
{
    public class CreateProposalModel
    {
        public string Reason { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rule { get; set; }
        public string Slogan { get; set; }
    }
}
