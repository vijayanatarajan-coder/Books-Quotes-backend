using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

namespace BackendApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<UserRegistration> UserRegistrations => Set<UserRegistration>();
         public DbSet<Quote> Quotes => Set<Quote>();
    }
}
