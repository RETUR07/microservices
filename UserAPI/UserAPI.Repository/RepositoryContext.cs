using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserAPI.Entities.Models;
using System;

namespace UserAPI.Repository.Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany(u => u.MakedFriend);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Subscribers)
                .WithMany(u => u.Subscribed);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(ch => ch.Users);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User);
        }
    }
}
