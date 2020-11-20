using System.Text.Json;
using WeETL.Core;
using WeETL.Schemas;

namespace WeETL
{
    public class TStringToJson<TOutputSchema> : ETLComponent<ContentSchema<string>, TOutputSchema>
        where TOutputSchema : class, new()
    {
        protected override TOutputSchema InternalInputTransform(ContentSchema<string> row)
        {
            var result = JsonSerializer.Deserialize<TOutputSchema>(row.Content);
            return result;
        }

    }
}
