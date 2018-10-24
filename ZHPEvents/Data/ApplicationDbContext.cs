using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Models;
using ZHPEvents.Models.Identity;

namespace ZHPEvents.Data
{
    public class ApplicationDbContext : IdentityDbContext<ZHPEventsUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ZHPEvents.Models.Event> Event { get; set; }
    }
}
