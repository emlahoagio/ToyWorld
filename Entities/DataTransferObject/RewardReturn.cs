namespace Entities.DataTransferObject
{
    public class RewardReturn
    {
        public int Id { get; set; }
        public PostOfContestInReward Post { get; set; }
        public PrizeReturn Prizes { get; set; }
    }
}
