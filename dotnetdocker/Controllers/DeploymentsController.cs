using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeploymentsController : ControllerBase
    {
        private readonly DeploymentContext _context;

        public DeploymentsController(DeploymentContext context)
        {
            _context = context;
        }

        // GET: api/Deployments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deployment>>> GetDeployments()
        {
            return await _context.Deployments.ToListAsync();
        }

        // GET: api/Deployments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deployment>> GetDeployment(string id)
        {
            var deployment = await _context.Deployments.FindAsync(id);

            if (deployment == null)
            {
                return NotFound();
            }

            return deployment;
        }

        // PUT: api/Deployments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeployment(string id, Deployment deployment)
        {
            if (id != deployment.DeploymentId)
            {
                return BadRequest();
            }

            _context.Entry(deployment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeploymentExists(id))
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

        // POST: api/Deployments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Deployment>> PostDeployment(Deployment deployment)
        {
            _context.Deployments.Add(deployment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeploymentExists(deployment.DeploymentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeployment", new { id = deployment.DeploymentId }, deployment);
        }

        // DELETE: api/Deployments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deployment>> DeleteDeployment(string id)
        {
            var deployment = await _context.Deployments.FindAsync(id);
            if (deployment == null)
            {
                return NotFound();
            }

            _context.Deployments.Remove(deployment);
            await _context.SaveChangesAsync();

            return deployment;
        }

        private bool DeploymentExists(string id)
        {
            return _context.Deployments.Any(e => e.DeploymentId == id);
        }
    }
}
