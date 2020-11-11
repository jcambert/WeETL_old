namespace WeETL.Tests
{
    public class ConvertSchemaFrom
    {
        public int Index { get; set; }
        public string AProperty { get; set; }
    }
    public class ConvertSchemaTo
    {
        [LogOrder(1)]
        public int Index { get; set; }
        [LogOrder(2)]
        public string  AnotherProperty { get; set; }
    }
}