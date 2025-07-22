using Microsoft.EntityFrameworkCore;
using SyntaxSugar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserProblemStatus> UserProblemStatuses { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProblemTag> ProblemTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();

            });

            modelBuilder.Entity<Problem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Difficulty).IsRequired();
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Problems)
                      .HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ProblemTag>(entity =>
            {
                entity.HasKey(pt => new { pt.ProblemId, pt.TagId });

                entity.HasOne(pt => pt.Problem)
                      .WithMany(p => p.ProblemTags)
                      .HasForeignKey(pt => pt.ProblemId);

                entity.HasOne(pt => pt.Tag)
                      .WithMany(t => t.ProblemTags)
                      .HasForeignKey(pt => pt.TagId);
            });

            modelBuilder.Entity<UserProblemStatus>(entity =>
            {
                entity.HasKey(pt => new { pt.UserId, pt.ProblemId });

                entity.HasOne(ups => ups.User)
                  .WithMany(u => u.UserProblemStatuses)
                  .HasForeignKey(ups => ups.UserId);

                entity.HasOne(ups => ups.Problem)
                .WithMany(p => p.UserProblemStatuses)
                .HasForeignKey(ups => ups.ProblemId);
            });
        }
    }
}
