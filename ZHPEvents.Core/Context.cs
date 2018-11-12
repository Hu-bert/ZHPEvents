using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Core.Entities;
using ZHPEvents.Core.Identity;

namespace ZHPEvents.Core
{
    public class Context: IdentityDbContext<AppUser, IdentityRole, string>
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Raport> Raport { get; set; }

    }
}
