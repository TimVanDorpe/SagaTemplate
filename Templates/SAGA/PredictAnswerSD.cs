using HC.Answer.Application.Command;
using HC.Answer.Processor.Saga.State;
using NServiceBus;
using System;

namespace HC.Answer.Processor.Saga
{
    public class PredictAnswerSD : ContainSagaData
    {
        public PredictAnswerSS State { get; set; }
        public Guid Identifier { get; set; }
        public PredictAnswerCMD Command { get; set; }

    }
}
