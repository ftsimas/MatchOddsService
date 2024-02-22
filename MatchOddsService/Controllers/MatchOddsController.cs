using AutoMapper;
using MatchOddsService.Models;
using MatchOddsService.Models.DTOs;
using MatchOddsService.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MatchOddsService.Controllers
{
    public class MatchOddsController : CRUDControllerBase<MatchOdds, MatchOddsDTO>
    {
        public MatchOddsController(MatchOddsServiceDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.MatchOdds, mapper) { }

        /// <summary>
        /// Retrieves all match odds entries
        /// </summary>
        /// <response code="200">All match odds entries retrieved</response>
        /// <response code="204">No match odds entries exist</response>
        [ProducesResponseType(typeof(ICollection<MatchOddsDTO>), 200)]
        public override IActionResult Get() => base.Get();

        /// <summary>
        /// Retrieves a match odds entry by ID
        /// </summary>
        /// <param name="id" example="1">The match odds entry id</param>
        /// <response code="200">Match odds entry retrieved</response>
        /// <response code="404">Match odds entry not found</response>
        [ProducesResponseType(typeof(MatchOddsDTO), 200)]
        [ProducesResponseType(typeof(ErrorDTO), 404)]
        public override IActionResult Get(long id) => base.Get(id);

        /// <summary>
        /// Creates a new match odds entry
        /// </summary>
        /// <response code="200">Match odds entry created</response>
        /// <response code="400">Invalid input</response>
        [ProducesResponseType(typeof(MatchOddsDTO), 200)]
        [ProducesResponseType(typeof(ErrorDTO), 400)]
        public override IActionResult Post([FromBody] MatchOddsDTO dto)
        {
            var matchExists = ((MatchOddsServiceDbContext)DbContext).Matches.Any(match => match.ID == dto.MatchId);
            if (!matchExists)
            {
                return ForeignKeyNotFound(nameof(dto.MatchId), dto.MatchId.Value);
            }
            return base.Post(dto);
        }

        /// <summary>
        /// Updates an existing match odds entry
        /// </summary>
        /// <param name="id" example="1">The match odds entry id</param>
        /// <response code="200">Match odds entry updated</response>
        /// <response code="400">Invalid input</response>
        [ProducesResponseType(typeof(MatchOddsDTO), 200)]
        [ProducesResponseType(typeof(ErrorDTO), 400)]
        public override IActionResult Put(long id, [FromBody] MatchOddsDTO dto) => base.Put(id, dto);

        /// <summary>
        /// Deletes an existing match odds entry
        /// </summary>
        /// <param name="id" example="1">The match odds entry id</param>
        /// <response code="204">Match odds entry deleted</response>
        /// <response code="404">Match odds entry not found</response>
        [ProducesResponseType(typeof(ErrorDTO), 404)]
        public override IActionResult Delete(long id) => base.Delete(id);
    }
}
