using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TShip.Models.Entities;

namespace TShip.Data
{
    //package manager console=>>
    //add-migration "update"
    //update-database
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}