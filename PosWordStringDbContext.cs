
using System.Data.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Common.Infrastructure.DomainPersistence.EF;
using HC.Isaac.Infrastructure.DomainPersistence.Entity;
using Microsoft.EntityFrameworkCore;

namespace HC.Isaac.Infrastructure.DomainPersistence.Repository.PosWordString
{
    public class PosWordStringDbContext : PersistenceContext
    {
        public PosWordStringDbContext(DbConnection connection) : base(connection)
        {
        }

        public PosWordStringDbContext(DbConnection connection, DbContextOptions options) : base(connection, options)
        {
        }

        // ONLY USE THIS CONSTRUCTOR FOR TEST PURPOSE !!
        public PosWordStringDbContext(DbContextOptions options)
            : base(null, options)
        {

        }
        public virtual DbSet<DomainEventStreamPE> DomainEventStream { get; set; }
        public virtual DbSet<ActivityLogPE> ActivityLogs { get; set; }
        public virtual DbSet<PosWordStringPE> PosWordStrings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new TrackedEntityConfiguration<DomainEventStreamPE>())
                .ApplyConfiguration(new TrackedEntityConfiguration<ActivityLogPE>())
                .ApplyConfiguration(new AggregateConfiguration<PosWordStringPE>())


                // MAPPER
                .ConfigurateAndMapDomainEventStream()               
                .Entity<PosWordStringPE>(x =>
                {
				 x.ToTable("PosWordString", "dbo");
									x.Property(p => p.Text)
					.IsRequired()
					.HasColumnType("nvarchar(256)");
					//TODO :  refactor DB type above (nvarchar(256))
					x.Property(p => p.Language)
					.IsRequired()
					.HasColumnType("nvarchar(256)");
					//TODO :  refactor DB type above (nvarchar(256))
					x.Property(p => p.DefaultPosPolarity)
					.IsRequired()
					.HasColumnType("nvarchar(256)");
					//TODO :  refactor DB type above (nvarchar(256))
                               
                })
                ;
        }
    }
}
