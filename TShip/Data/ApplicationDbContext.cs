using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TShip.Models.Entities;

namespace TShip.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

    }
}