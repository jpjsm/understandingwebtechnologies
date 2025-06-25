using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DeploymentContext:DbContext
    {
        public DeploymentContext(DbContextOptions<DeploymentContext> options)
            :base(options)
        {
            // Empty body
        }

        public DbSet<Deployment> Deployments { get; set; }
    }
}
