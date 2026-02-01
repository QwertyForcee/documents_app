using DocumentsAPI.Core.Documents.Models;
using DocumentsAPI.Core.Statistics.Models;
using DocumentsAPI.Core.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            builder.Entity<Document>()
                .HasOne(d => d.Owner)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Comment>()
                .HasOne(c => c.Document)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserStatistics>(entity =>
            {
                entity.HasKey(us => new { us.UserId, us.Year });

                entity.HasOne(us => us.User)
                      .WithMany(u => u.Statistics)
                      .HasForeignKey(us => us.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
