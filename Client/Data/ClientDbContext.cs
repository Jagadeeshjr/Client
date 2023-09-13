using Client.Model;
using ClientNamespace.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClientNamespace.Data
{
    public class ClientDbContext : IdentityDbContext<User>
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options)
            : base(options)
        {

        }

        public DbSet<ClientModel> Clients { get; set; }
    }
}
