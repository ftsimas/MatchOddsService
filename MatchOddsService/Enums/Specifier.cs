using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MatchOddsService.Enums
{
    public enum Specifier
    {
        [Display(Name = "X")]
        Draw = 0,
        [Display(Name = "1")]
        TeamAWin = 1,
        [Display(Name = "2")]
        TeamBWin = 2
    }
}
