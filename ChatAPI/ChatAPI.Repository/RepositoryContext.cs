using Microsoft.EntityFrameworkCore;
using ChatAPI.Entities.Models;
using System;

namespace ChatAPI.Repository.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {
        }

        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Chat>()
                .HasMany(x => x.UserIds)
                .WithMany(x => x.Chats);
        }
    }
}
