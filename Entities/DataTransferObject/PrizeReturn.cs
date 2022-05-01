using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class PrizeReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public List<ImageReturn> Images { get; set; }
    }
}
