using AutoMapper;
using MatchOddsService.Enums;
using MatchOddsService.Models.DTOs;
using MatchOddsService.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using EDTOF = MatchOddsService.Factories.ErrorDTOFactory;

namespace MatchOddsService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CRUDControllerBase<TEntity, TDTO> : ControllerBase where TEntity : EntityBase where TDTO : DTOBase
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _entities;
        private readonly IMapper _mapper;

        protected CRUDControllerBase(DbContext dbContext, DbSet<TEntity> entities, IMapper mapper)
        {
            _dbContext = dbContext;
            _entities = entities;
            _mapper = mapper;
        }

        protected DbContext DbContext { get => _dbContext; }
        protected DbSet<TEntity> Entities { get => _entities; }
        protected IMapper Mapper { get => _mapper; }

        [NonAction]
        public virtual NotFoundObjectResult NotFound(long id) =>
            NotFound(EDTOF.NotFoundError(new KeyValuePair<string, object>(typeof(TEntity).Name, id)));

        [NonAction]
        public virtual BadRequestObjectResult MapperProblem(AutoMapperMappingException ex) =>
            BadRequest(EDTOF.DataValidationError(ex.MemberMap.SourceMembers.First().Name));

        [NonAction]
        public virtual BadRequestObjectResult DbUpdateProblem(DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlException)
            {
                switch ((SqlServerErrorCode)sqlException.Number)
                {
                    case SqlServerErrorCode.ForeignKeyConstraint:
                        return BadRequest(EDTOF.ForeignKeyError());
                    case SqlServerErrorCode.UniqueIndexDuplicateKey:
                        return BadRequest(EDTOF.UniqueIndexError());
                }
            }
            throw ex;
        }

        [NonAction]
        public virtual BadRequestObjectResult ForeignKeyNotFound(string name, long id) =>
            BadRequest(EDTOF.ForeignKeyError(new KeyValuePair<string, object>(name, id)));

        [NonAction]
        public virtual BadRequestObjectResult ForeignKeyRestrictionOnDelete(string resourceName, long id, string referencingResourceName = null) =>
            BadRequest(EDTOF.ForeignKeyRestrictionOnDeleteError(new KeyValuePair<string, object>(resourceName, id), referencingResourceName));

        [HttpGet]
        public virtual IActionResult Get()
        {
            if (_entities.Count() > 0)
            {
                return Ok(_entities.Select(entity => _mapper.Map<TDTO>(entity)));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(long id)
        {
            var entity = _entities.Find(id);
            if (entity == null)
            {
                return NotFound(id);
            }
            else
            {
                return Ok(_mapper.Map<TDTO>(entity));
            }
        }

        [HttpPost]
        public virtual IActionResult Post([FromBody] TDTO dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                _entities.Add(entity);
                _dbContext.SaveChanges();
                return Ok(_mapper.Map<TDTO>(entity));
            }
            catch (AutoMapperMappingException ex)
            {
                return MapperProblem(ex);
            }
            catch (DbUpdateException ex)
            {
                return DbUpdateProblem(ex);
            }
        }

        [HttpPut("{id}")]
        public virtual IActionResult Put(long id, [FromBody] TDTO dto)
        {
            if (dto.ID != id)
            {
                return BadRequest(EDTOF.IdMismatchError());
            }

            if (!_entities.Any(entity => entity.ID == id))
            {
                return NotFound(id);
            }

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                _entities.Update(entity);
                _dbContext.SaveChanges();
                return Ok(_mapper.Map<TDTO>(entity));
            }
            catch (AutoMapperMappingException ex)
            {
                return MapperProblem(ex);
            }
            catch (DbUpdateException ex)
            {
                return DbUpdateProblem(ex);
            }
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(long id)
        {
            var entity = _entities.Find(id);
            if (entity == null)
            {
                return NotFound(id);
            }
            else
            {
                _entities.Remove(entity);
                _dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
