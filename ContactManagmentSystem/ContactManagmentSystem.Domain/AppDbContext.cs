using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.EFCore.Extensions;
using ContactManagementSystem.Core;
using ContactManagementSystem.Domain.Interfaces;
using ContactManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementSystem.Domain
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTime _dateTime;

        public AppDbContext() : base(CreateOptions(null))
        { }

        public AppDbContext(
              DbContextOptions options,
              IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }
        public DbSet<Contact> Contacts { get; set; }
        #region DbSets



        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
            
            modelBuilder.Entity<BaseEntity>()
                    .Property(b => b.CreatedBy)
                    .IsRequired(false);

            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.ModifiedBy)
                .IsRequired(false);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                // and modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            #region SeedData
            modelBuilder.Entity<Contact>().HasData(SeedData.Hesho);

            #endregion
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditInfo();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetAuditInfo();

            return base.SaveChanges();
        }

        private void SetAuditInfo()
        {
            foreach (var entry in ChangeTracker.Entries<BaseAudit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.Now;
                        entry.Entity.ModifiedDate =
                        entry.Entity.CreatedDate;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = _dateTime.Now;
                        break;
                }
            }
        }

        private static DbContextOptions<AppDbContext> CreateOptions(string connName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            if (connName == null)
            {
                // DbContext configuration
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=contactManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
            else
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=contactManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
            return optionsBuilder.Options;
        }
    }
}