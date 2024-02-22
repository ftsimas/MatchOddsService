using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatchOddsService.Models.DTOs
{
    [DisplayName("MatchOdds")]
    public class MatchOddsDTO : DTOBase
    {
        /// <summary>
        /// The match's ID
        /// </summary>
        /// <example>1</example>
        [Required]
        public long? MatchId { get; set; }

        /// <summary>
        /// The match odds' specifier
        /// </summary>
        /// <example>X</example>
        [Required]
        public string Specifier { get; set; }

        /// <summary>
        /// The match's odds
        /// </summary>
        /// <example>1.5</example>
        [Required]
        public decimal? Odd { get; set; }
    }
}
