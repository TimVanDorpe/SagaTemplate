namespace HC.Answer.Processor.Saga.State
{
    public enum PredictAnswerSS
    {
        Start,
        Step1Done,
        Step2Done,
     
    }

    public static class PredictAnswerState
    {     
        public static int Total = 2;
        public static string Step1 = "Step1";
        public static string Step2 = "Step2";
  
    }
}
