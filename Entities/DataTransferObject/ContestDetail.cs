using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class ContestDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
        public bool IsOnlineContest { get; set; }
        public double? RegisterCost { get; set; }
        public int? MinRegistration { get; set; }
        public int? MaxRegistration { get; set; }
        public DateTime? StartRegistration { get; set; }
        public DateTime? EndRegistration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
        public List<EvaluateInContestDetail> Evaluates { get; set; }
    }
}
