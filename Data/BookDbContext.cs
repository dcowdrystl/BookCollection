using BookCollection.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookCollection.Data
{
    public class BookDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BookUser> BookUsers { get; set; }

        public DbSet<Friends> Friends { get; set; }

        public DbSet<UserProfile> Profiles { get; set; }
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        { 
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookUser>()
                .HasKey(bu => new { bu.BookId, bu.ApplicationUserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}