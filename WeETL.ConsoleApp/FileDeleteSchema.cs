using WeETL.Schemas;

namespace WeETL.ConsoleApp
{
    public class FileDeleteSchema : IFilenameSchema
    {
        public string Filename { get ; set ; }
    }
}
