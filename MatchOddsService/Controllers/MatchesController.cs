using AutoMapper;
using MatchOddsService.Models;
using MatchOddsService.Models.DTOs;
using MatchOddsService.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace MatchOddsService.Controllers
{
    public class MatchesController : CRUDControllerBase<Match, MatchDTO>
    {
        public MatchesController(MatchOddsServiceDbContext context, IMapper mapper) : base(context, context.Matches, mapper) { }

        /// <summary>
        /// Retrieves all matches
        /// </summary>
        /// <response code="200">All matches retrieved</response>
        /// <response code="204">No matches exist</response>
        [ProducesResponseType(typeof(ICollection<MatchDTO>), 200)]
        public override IActionResult Get() => base.Get();

        /// <summary>
        /// Retrieves a match by ID
        /// </summary>
        /// <param name="id" example="1">The match id</param>
        /// <response code="200">Match retrieved</response>
        /// <response code="404">Match not found</response>
        [ProducesResponseType(typeof(MatchDTO), 200)]
        [ProducesResponseType(typeof(ErrorDTO), 404)]
        public override IActionResult Get(long id) => base.Get(id);

        /// <summary>
        /// Creates a new match
        /// </summary>
        /// <response code="200">Match created</response>
        /// <response code="400">Invalid input</response>
        [ProducesResponseType(typeof(MatchDTO), 200)]
        [ProducesResponseType(typeof(ErrorDTO), 400)]
        public override IActionResult Post([FromBody] MatchDTO dto) => base.Post(dto);

        /// <summary>
        /// Updates an existing match
        /// </summary>
        /// <param name="id" example="1">The match id</param>
        /// <response code="200">Match updated</response>
        /// <response code="400">Invalid input</response>
        [ProducesResponseType(typeof(MatchDTO), 200)]
        [ProducesResponseType(typeof(ErrorDTO), 400)]
        public override IActionResult Put(long id, [FromBody] MatchDTO dto) => base.Put(id, dto);

        /// <summary>
        /// Deletes an existing match
        /// </summary>
        /// <param name="id" example="1">The match id</param>
        /// <response code="204">Match deleted</response>
        /// <response code="404">Match not found</response>
        [ProducesResponseType(typeof(ErrorDTO), 404)]
        public override IActionResult Delete(long id) {
            var matchOddsExist = ((MatchOddsServiceDbContext)DbContext).MatchOdds.Any(mo => mo.MatchId == id);
            if (matchOddsExist)
            {
                return ForeignKeyRestrictionOnDelete(nameof(Match), id, nameof(MatchOdds));
            }
            return base.Delete(id);
        }
    }
}
