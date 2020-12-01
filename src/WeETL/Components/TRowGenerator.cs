using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Observables;

namespace WeETL
{
    public class TRowGenerator<TSchema> : ETLStartableComponent<TSchema, TSchema>
        where TSchema : class, new()
    {
   

         
        public TRowGenerator() : base()
        {
        
        }

        public RowGeneratorOptions<TSchema> Options { get; set; } = new RowGeneratorOptions<TSchema>();

        public int NumberOfRowToGenerate { get; set; } = 10;

        protected override Task InternalStart(CancellationToken token)
        {
            RowGenerator<TSchema> gen = new RowGenerator<TSchema>(Options);
            gen.NumberOfRowToGenerate = NumberOfRowToGenerate;
            gen.Subscribe(OutputHandler,token);

           
            return Task.CompletedTask;
        }

     

       
    }
}
