using MatchOddsService.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatchOddsService.Models.DTOs
{
    [DisplayName("Match")]
    public class MatchDTO : DTOBase
    {
        /// <summary>
        /// A description of the match
        /// </summary>
        /// <example>OSFP-PAO</example>
        public string Description { get; set; }

        /// <summary>
        /// The match's date
        /// </summary>
        /// <example>2021-03-19</example>
        [Required]
        public string MatchDate { get; set; }

        /// <summary>
        /// The match's time
        /// </summary>
        /// <example>12:00</example>
        [Required]
        public string MatchTime { get; set; }

        /// <summary>
        /// The match's home team
        /// </summary>
        /// <example>OSFP</example>
        [Required]
        public string TeamA { get; set; }

        /// <summary>
        /// The match's away team
        /// </summary>
        /// <example>PAO</example>
        [Required]
        public string TeamB { get; set; }

        /// <summary>
        /// The match's sport
        /// </summary>
        /// <example>1</example>
        [Required]
        public Sport? Sport { get; set; }
    }
}
