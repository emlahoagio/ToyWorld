using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class PrizeOfContest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public List<ImageReturn> Images { get; set; }
    }
}
