using HC.Answer.Application.Command;
using HC.Answer.Processor.Saga.State;
using NServiceBus;
using System;

namespace HC.Answer.Processor.Saga
{
    public class ParticipantAnswerSD : ContainSagaData
    {
        public ParticipantAnswerSS State { get; set; }
        public Guid Identifier { get; set; }
        public ParticipantAnswerCMD Command { get; set; }

    }
}
