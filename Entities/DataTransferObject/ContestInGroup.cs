using System;
using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class ContestInGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MinRegistration { get; set; }
        public int? MaxRegistration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartRegistration { get; set; }
        public DateTime? EndRegistration { get; set; }
        public int Status { get; set; }
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
        public List<PrizeOfContest> Prizes { get; set; }
    }
}
