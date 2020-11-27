namespace WeETL.Schemas
{
    public class FilenameSchema : IFilenameSchema
    {
        public string Filename { get ; set; }

        public override string ToString()
        => Filename;
    }
}
