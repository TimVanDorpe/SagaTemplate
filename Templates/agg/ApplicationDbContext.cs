  using HC.Common.Infrastructure.DomainPersistence;
using HC.Common.Infrastructure.DomainPersistence.EF;
using HC.Common.Infrastructure.DomainPersistence.Extensions;
using HC.Common.Infrastructure.Sharding;
using HC.LegacySync.Infrastructure.DomainPersistence.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace HC.LegacySync.Infrastructure
{
    public class ApplicationDbContext : PersistenceContext
    {
        public ApplicationDbContext(DbConnection connection, ShardingInterceptor interceptor)
            : base(connection, interceptor) { }

        public ApplicationDbContext(DbConnection connection, DbContextOptions options, ShardingInterceptor interceptor)
            : base(connection, options, interceptor) { }

        // ONLY USE THIS CONSTRUCTOR FOR TEST PURPOSE !!
        public ApplicationDbContext(DbContextOptions options, ShardingInterceptor interceptor)
            : base(null, options, interceptor) { }

        public virtual DbSet<DomainEventStreamPE> DomainEventStream { get; set; }
        public virtual DbSet<ActivityLogPE> ActivityLogs { get; set; }
        public virtual DbSet<SurveyPE> Tenant { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new TrackedEntityConfiguration<ActivityLogPE>())
                .ApplyConfiguration(new TrackedEntityConfiguration<DomainEventStreamPE>())
                .ApplyConfiguration(new TrackedEntityConfiguration<SurveyPE>())
                .ConfigurateAndMapDomainEventStream()

                // MAPPER
                .Entity<SurveyPE>(x =>
                {
                    x.ToTable("Survey", "dbo");

                });              
        }
    }
}
  