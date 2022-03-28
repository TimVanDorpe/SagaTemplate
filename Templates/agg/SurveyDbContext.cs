

using System.Data.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Common.Infrastructure.DomainPersistence.EF;
using HC.LegacySync.Infrastructure.DomainPersistence.Entity;
using Microsoft.EntityFrameworkCore;

namespace HC.LegacySync.Infrastructure.DomainPersistence.Repository
{
    public class SurveyDbContext : PersistenceContext
    {
        public SurveyDbContext(DbConnection connection) : base(connection)
        {
        }

        public SurveyDbContext(DbConnection connection, DbContextOptions options) : base(connection, options)
        {
        }

        // ONLY USE THIS CONSTRUCTOR FOR TEST PURPOSE !!
        public SurveyDbContext(DbContextOptions options)
            : base(null, options)
        {

        }
        public virtual DbSet<DomainEventStreamPE> DomainEventStream { get; set; }
        public virtual DbSet<ActivityLogPE> ActivityLogs { get; set; }
        public virtual DbSet<SurveyPE> Surveys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new TrackedEntityConfiguration<DomainEventStreamPE>())
                .ApplyConfiguration(new TrackedEntityConfiguration<ActivityLogPE>())
                .ApplyConfiguration(new AggregateConfiguration<SurveyPE>())


                // MAPPER
                .ConfigurateAndMapDomainEventStream()               
                .Entity<SurveyPE>(x =>
                {
				 x.ToTable("Survey", "dbo");
									x.Property(p => p.TouchpointUniqueId)
					.IsRequired()
					.HasColumnType("nvarchar(256)");
					//TODO :  refactor DB type above (nvarchar(256))
                               
                })
                ;
        }
    }
}
