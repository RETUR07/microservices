using Microsoft.EntityFrameworkCore;
using PostAPI.Entities.Models;
using System;

namespace PostAPI.Repository.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Rate>()
                .Property(e => e.LikeStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (LikeStatus)Enum.Parse(typeof(LikeStatus), v));

            modelBuilder
                .Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Rates)
                .WithOne(r => r.User);

            modelBuilder
                .Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.ParentPost);
        }
    }
}
