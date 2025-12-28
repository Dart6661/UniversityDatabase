using DatabaseModels.UnivModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.Context
{
    public class UnivContext(DbContextOptions<UnivContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Curator> Curators { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("groups");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id).ValueGeneratedOnAdd();
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
                entity.Property(g => g.CreationDate).IsRequired();
            });

            modelBuilder.Entity<Curator>(entity =>
            {
                entity.ToTable("curators");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(200);

                entity.HasOne(c => c.Group)
                      .WithOne(g => g.Curator)
                      .HasForeignKey<Curator>(c => c.GroupId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Age).IsRequired();

                entity.HasOne(s => s.Group)
                      .WithMany(g => g.Students)
                      .HasForeignKey(s => s.GroupId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
