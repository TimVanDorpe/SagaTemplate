using HC.Answer.Application.Command;
using HC.Answer.Processor.Saga.State;
using NServiceBus;
using System;

namespace HC.Answer.Processor.Saga
{
    public class PredicteAnswerSD : ContainSagaData
    {
        public PredicteAnswerSS State { get; set; }
        public Guid Identifier { get; set; }
        public PredicteAnswerCMD Command { get; set; }

    }
}
