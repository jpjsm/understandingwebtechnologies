using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFFromData.DbCtx;
using EFFromData.DbModel;

namespace EFFromData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionRequestTablesController : ControllerBase
    {
        private readonly fcmchangemanagersqlContext _context;

        public ExceptionRequestTablesController(fcmchangemanagersqlContext context)
        {
            _context = context;
        }

        // GET: api/ExceptionRequestTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExceptionRequestTable>>> GetExceptionRequests()
        {
            return await _context.ExceptionRequests.ToListAsync();
        }

        // GET: api/ExceptionRequestTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExceptionRequestTable>> GetExceptionRequestTable(Guid id)
        {
            var exceptionRequestTable = await _context.ExceptionRequests.FindAsync(id);

            if (exceptionRequestTable == null)
            {
                return NotFound();
            }

            return exceptionRequestTable;
        }

        // PUT: api/ExceptionRequestTables/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExceptionRequestTable(Guid id, ExceptionRequestTable exceptionRequestTable)
        {
            if (id != exceptionRequestTable.ExceptionRequestId)
            {
                return BadRequest();
            }

            _context.Entry(exceptionRequestTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExceptionRequestTableExists(id))
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

        // POST: api/ExceptionRequestTables
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExceptionRequestTable>> PostExceptionRequestTable(ExceptionRequestTable exceptionRequestTable)
        {
            _context.ExceptionRequests.Add(exceptionRequestTable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExceptionRequestTableExists(exceptionRequestTable.ExceptionRequestId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExceptionRequestTable", new { id = exceptionRequestTable.ExceptionRequestId }, exceptionRequestTable);
        }

        // DELETE: api/ExceptionRequestTables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExceptionRequestTable>> DeleteExceptionRequestTable(Guid id)
        {
            var exceptionRequestTable = await _context.ExceptionRequests.FindAsync(id);
            if (exceptionRequestTable == null)
            {
                return NotFound();
            }

            _context.ExceptionRequests.Remove(exceptionRequestTable);
            await _context.SaveChangesAsync();

            return exceptionRequestTable;
        }

        private bool ExceptionRequestTableExists(Guid id)
        {
            return _context.ExceptionRequests.Any(e => e.ExceptionRequestId == id);
        }
    }
}
