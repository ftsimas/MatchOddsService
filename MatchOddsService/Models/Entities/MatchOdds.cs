using MatchOddsService.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchOddsService.Models.Entities
{
    public class MatchOdds : EntityBase
    {
        private const string OddDecimalPrecision = "9";
        private const string OddDecimalScale = "2";

        private decimal _odd;

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; }

        public long MatchId { get; set; }

        public Specifier Specifier { get; set; }

        [Column(TypeName = "decimal(" + OddDecimalPrecision + ", " + OddDecimalScale + ")")]
        public decimal Odd {
            get => _odd;
            set => _odd = Math.Round(value, Int32.Parse(OddDecimalScale), MidpointRounding.ToEven);
        }
    }
}
