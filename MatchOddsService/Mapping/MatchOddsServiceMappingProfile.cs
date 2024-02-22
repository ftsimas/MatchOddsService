using AutoMapper;
using MatchOddsService.Models.DTOs;
using MatchOddsService.Models.Entities;
using System;
using System.Globalization;
using MatchOddsService.Enums;
using MatchOddsService.Helpers;

namespace MatchOddsService.Mapping
{
    public class MatchOddsServiceMappingProfile : Profile
    {
        public MatchOddsServiceMappingProfile()
        {
            CreateMap<Match, MatchDTO>()
                .ForMember(dto => dto.MatchDate, options => options.MapFrom(entity =>
                    entity.MatchDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)))
                .ForMember(dto => dto.MatchTime, options => options.MapFrom(entity =>
                    entity.MatchTime.ToString(@"hh\:mm")));
            CreateMap<MatchDTO, Match>()
                .ForMember(entity => entity.Sport, options => options.MapFrom<SportFromDTOResolver>());
            CreateMap<MatchOdds, MatchOddsDTO>()
                .ForMember(dto => dto.Specifier, options => options.MapFrom(
                    entity => entity.Specifier.ToDTOString()));
            CreateMap<MatchOddsDTO, MatchOdds>()
                .ForMember(entity => entity.Specifier, options => options.MapFrom(
                    dto => SpecifierHelper.FromDTOString(dto.Specifier)));
        }

        private class SportFromDTOResolver : IValueResolver<MatchDTO, Match, Sport>
        {
            public Sport Resolve(MatchDTO source, Match destination, Sport member, ResolutionContext context) =>
                Enum.IsDefined(typeof(Sport), source.Sport) ? source.Sport.Value : throw new InvalidOperationException();
        }
    }
}
