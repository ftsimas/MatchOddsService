using MatchOddsService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchOddsService.Models.Entities
{
    public class Match : EntityBase
    {
        private DateTime _matchDate;
        private TimeSpan _matchTime;

        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime MatchDate {
            get => _matchDate;
            set => _matchDate = value.Date;
        }

        [Column(TypeName = "time(0)")]
        public TimeSpan MatchTime {
            get => _matchTime;
            set => _matchTime = new TimeSpan(value.Hours, value.Minutes, 0);
        }

        [Required]
        public string TeamA { get; set; }

        [Required]
        public string TeamB { get; set; }

        public Sport Sport { get; set; }

        public IList<MatchOdds> MatchOdds { get; set; }
    }
}
