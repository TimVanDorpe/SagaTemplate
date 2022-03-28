namespace HC.Answer.Processor.Saga.State
{
    public enum PredicteAnswerSS
    {
        Start,
        Step1Done,
        Step2Done,
     
    }

    public static class PredicteAnswerState
    {     
        public static int Total = 2;
        public static string Step1 = "Step1";
        public static string Step2 = "Step2";
  
    }
}
