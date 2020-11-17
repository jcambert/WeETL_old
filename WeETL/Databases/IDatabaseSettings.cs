namespace WeETL.Databases
{
    public interface IDatabaseSettings<T>
    {
        public T Settings { get; set; }

        public string DatabaseName { get; set; }
    }
}
