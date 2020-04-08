namespace HC.Swatson.Processor.Saga.State
{
    public enum RunUnitTestSS
    {
        Start,
        Step1Done,
        Step2Done,
        Step3Done,
     
    }

    public static class RunUnitTestState
    {     
        public static int Total = 3;
        public static string Step1 = "Step1";
        public static string Step2 = "Step2";
        public static string Step3 = "Step3";
  
    }
}
