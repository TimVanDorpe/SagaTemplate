using HC.Swatson.Application.Command;
using HC.Swatson.Processor.Saga.State;
using NServiceBus;
using System;

namespace HC.Swatson.Processor.Saga
{
    public class RunUnitTestSD : ContainSagaData
    {
        public RunUnitTestSS State { get; set; }
        public Guid Identifier { get; set; }
        public RunUnitTestCMD Command { get; set; }

    }
}
