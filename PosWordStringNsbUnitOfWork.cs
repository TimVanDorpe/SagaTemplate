
using System;
using HC.Common.Infrastructure.DomainPersistence.EF;
using NServiceBus.Persistence;
namespace HC.Isaac.Infrastructure.DomainPersistence.Repository.PosWordString
{
    public class PosWordStringNsbUnitOfWork : NsbUnitOfWorkBehavior<PosWordStringDbContext>
    {
        public PosWordStringNsbUnitOfWork(Func<SynchronizedStorageSession, PosWordStringDbContext> contextFactory)
            : base(contextFactory)
        {

        }
    }
}
