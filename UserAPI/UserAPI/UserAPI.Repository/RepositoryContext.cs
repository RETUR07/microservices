using Microsoft.EntityFrameworkCore;
using UserAPI.Entities.Models;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UserAPI.Repository.Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
