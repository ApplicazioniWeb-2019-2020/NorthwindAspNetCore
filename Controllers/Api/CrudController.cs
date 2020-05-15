using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAspNetCore.Infrastructure;
using NorthwindAspNetCore.Models;
using NorthwindAspNetCore.ViewModels;

namespace NorthwindAspNetCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CrudController<TContext, TResource, TQuery> : ControllerBase
        where TContext : DbContext
        where TResource : class, IHasId
        where TQuery : Query
    {
        private readonly TContext _context;
        private DbSet<TResource> _table;

        public CrudController(TContext context)
        {
            _context = context;
        }

        private DbSet<TResource> Table => _table ?? (_table = _context.Set<TResource>());

        // GET: api/<Resource>
        [HttpGet]
        public async Task<ActionResult<QueryResponse<TResource>>> GetAll([FromQuery] TQuery query)
        {
            return await Table.ToQueryResponse(query);
        }

        // GET: api/<Resource>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TResource>> GetSingle(int id)
        {
            var resource = await Table.FindAsync(id);

            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // PUT: api/<Resource>/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TResource resource)
        {
            if (id != resource.Id)
            {
                return BadRequest();
            }

            _context.Entry(resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/<Resource>
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TResource>> Create(TResource resource)
        {
            Table.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSingle", new { id = resource.Id }, resource);
        }

        // DELETE: api/<Resource>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TResource>> Delete(int id)
        {
            var resource = await Table.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            Table.Remove(resource);
            await _context.SaveChangesAsync();

            return resource;
        }

        private bool Exists(int id)
        {
            return Table.Any(e => e.Id == id);
        }
    }
}