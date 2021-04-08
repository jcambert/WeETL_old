using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using WeETL.IO;
using WeETL.Utilities;
namespace WeETL.Observables.PN2000.IO
{
    public interface IPnCadReader : IFileReader<IPnCadDocument>
    {
        
    }
    public class PnCadReader : FileLineReader<IPnCadDocument>, IPnCadReader
    {
        
        public PnCadReader( IPnCadDocument document, IFileReadLine lineReader) :base(document,lineReader)
        {
            OnLine.Where(line => !string.IsNullOrWhiteSpace(line)).Subscribe(line => {
               
                var values = Regex.Split(line,@"\s+");
                if (values.Length != 14)
                    return;
                PnCadSchema schema = new PnCadSchema()
                {
                    Color = values[1].TrySetIntValue() ?? 0,
                    Prop2 = values[2].TrySetIntValue() ?? 0,
                    Prop3 = values[3].TrySetIntValue() ?? 0,
                    Prop4 = values[4].TrySetIntValue() ?? 0,
                    Prop5 = values[5].TrySetIntValue() ?? 0,
                    Prop6 = values[6].TrySetIntValue() ?? 0,
                    Type = values[7].TrySetIntValue() ?? 0,
                    Direction = values[8].TrySetIntValue() ?? 0,
                    Point1X = values[9].TrySetDoubleValue() ?? 0.0,
                    Point1Y = values[10].TrySetDoubleValue() ?? 0.0,
                    Point2X = values[11].TrySetDoubleValue() ?? 0.0,
                    Point2Y = values[12].TrySetDoubleValue() ?? 0.0,
                    Angle = values[13].TrySetDoubleValue() ?? 0.0
                };
                document.Schema.Add(schema);
            });
        }
        
    }
}
