using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Core.Entities;
using ZHPEvents.Core.Identity;

namespace ZHPEvents.Core
{
    public class Context: IdentityDbContext<AppUser, IdentityRole, string>
    {
        private readonly DbContextOptions<Context> _options;


        public  DbSet<Event> Event { get; set; }
        public  DbSet<Raport> Raport { get; set; }

        public Context(DbContextOptions<Context> _options) : base(_options) { }


    }
}
