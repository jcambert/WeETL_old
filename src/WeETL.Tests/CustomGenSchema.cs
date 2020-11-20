namespace WeETL.Tests
{
    public class CustomGenSchema
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public override string ToString() => $"{Month}-{Year}";
    }
}
