using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class CreateContestParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Title { get; set; }
        [Required]
        [MinLength(6)]
        public string Description { get; set; }
        [Required]
        public string CoverImage { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Slogan { get; set; }
        [Required]
        public string Rule { get; set; }
        [Required]
        public DateTime? StartRegistration { get; set; }
        [Required]
        public DateTime? EndRegistration { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string TypeName { get; set; }
    }
}
