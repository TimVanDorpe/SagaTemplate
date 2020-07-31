using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Isaac.Domain.Aggregate.Alias;
using HC.Isaac.Infrastructure.DomainPersistence.Repository.Alias;

namespace HC.Isaac.Application.Command
{
    public class UpdateAAliasCH : CommandHandler<UpdateAAliasCMD, AliasAR>
    {
        private readonly AliasRepository _repository;

        public UpdateAAliasCH(AliasRepository repository)
            : base(repository)
        {
            this._repository = repository;
        }

        protected override async Task ExecuteAsync(UpdateAAliasCMD command)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Log line
            this.Log.Debug("Handle command", command.CorrelationUniqueId.ToUniqueId(), command);

            // Get the aggregate
            var aggregate = await _repository.FindAsync(Loading.Full, command.TenantUniqueId.ToTenantUniqueId(),
                command.UniqueId);

            // Update the aggregate
            aggregate = aggregate.Update();

            // Save aggregate to DB
            await this._repository.SaveAsync(command.CorrelationUniqueId.ToUniqueId(), command.UserUniqueId.ToUniqueId(), aggregate);

            // Log line
            this.Log.Debug("Handling command is done", command.CorrelationUniqueId.ToUniqueId());

        }
    }
}
