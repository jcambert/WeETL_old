using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Core;
using WeETL.Exceptions;
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

        protected override Task InternalStart(CancellationTokenSource tokenSource)
        {
            RowGenerator<TSchema> gen = new RowGenerator<TSchema>(Options);
            gen.NumberOfRowToGenerate = NumberOfRowToGenerate;
            gen.Subscribe(OutputHandler,TokenSource.Token);

           
            return Task.CompletedTask;
        }

     

       
    }
}
