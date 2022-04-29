namespace Entities.Models
{
    public partial class FavoriteType
    {
        public int TypeId { get; set; }
        public int AccountId { get; set; }

        public virtual Type Type { get; set; }
        public virtual Account Account { get; set; }
    }
}
