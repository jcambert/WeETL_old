using System.IO;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Schemas;

namespace WeETL
{
    public class TInputFile : ETLStartableComponent<string, ContentSchema<string>>
    {
        public string Filename { get; set; }
        protected override Task InternalStart()
        {
            string value= File.ReadAllText(Filename);
            ContentSchema<string> result = new ContentSchema<string>() { Content = value };
            InternalSendOutput(1, result);


            return Task.CompletedTask;
        }
    }
}
