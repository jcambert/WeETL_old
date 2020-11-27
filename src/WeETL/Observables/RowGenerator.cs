using AutoMapper;
using System;
using System.Reactive.Linq;
using WeETL.Utilities;
using System.Reactive.Disposables;
using System.Linq;
using WeETL.Exceptions;
using System.Threading;
using System.Threading.Tasks;
#if DEBUG
using System.Diagnostics;
#endif
namespace WeETL.Observables
{
    public class RowGenerator<T> : AbstractObservable<T>, IDisposable
         where T : class, new()
    {

        private T _lastRow;
        private bool disposedValue;
        private readonly IDisposable _lasRowOberver;
        public RowGeneratorOptions<T> Options { get; }
        public IMapper Mapper { get; }

        public int NumberOfRowToGenerate { get; set; } = 10;
        public bool Strict { get; set; }

        public RowGenerator(RowGeneratorOptions<T> options)
        {
            Check.NotNull(options, nameof(Options));
            this.Options = options;
            _lasRowOberver = Output.Subscribe(row => _lastRow = row);
        }

        public RowGenerator(IMapper mapper, RowGeneratorOptions<T> options) : this(options)
        {

            this.Mapper = mapper ?? new MapperFactory().AddMapper<T, T>().Build();
            Check.NotNull(Mapper, nameof(Mapper));
        }
        protected override IObservable<T> CreateOutputObservable()
       => Observable.Defer(() =>
       {
#if DEBUG
           Debug.WriteLine($"Constructing {nameof(RowGenerator<T>)} stream");
#endif
           
           return Observable.Create<T>((o, ct) =>
           {
               return Task.Run(() =>
               {

                   if (Strict && !(typeof(T).GetProperties().Select(p => p.Name).All(p => Options.Actions.ContainsKey(p))))
                   {
                       o.OnError(new ValidationException("In Strict mode, you must have a generator for each property"));

                   }
                   else
                   {
                       for (int i = 0; i < NumberOfRowToGenerate; i++)
                       {
                           ct.ThrowIfCancellationRequested();
                           o.OnNext(Generate());
                       }
                       o.OnCompleted();
                   }

               });

           });



       });

        public virtual T Generate()
        {
            T row = CreateNewRow();
            foreach (var property in Options.Actions.Keys)
            {
                var value = Options.Actions[property].Action(this, row);
                row.SetPropertyValue(property, value);
            }
            return row;
        }

        protected virtual T CreateNewRow()
        {
            Func<T> CreateRow = () =>
            {
                var res = new T();
                Options.InitFunction?.Invoke(res);
                return res;
            };
            return Options.Retain ? Mapper.Map(_lastRow ?? CreateRow()) : CreateRow();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                    _lasRowOberver?.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~RowGenerator()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
