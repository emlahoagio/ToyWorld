namespace Entities.DataTransferObject
{
    public class UpdateTradingPost
    {
        public string Title { get; set; }
        public string ToyName { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string Exchange { get; set; }
        public decimal? Value { get; set; }
        public string Phone { get; set; }
    }
}
