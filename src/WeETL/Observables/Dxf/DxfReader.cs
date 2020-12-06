using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public interface IDxfReader
    {
        void Load(string filename);
        IDxfDocument Document { get; }

        IObservable<IDxfDocument> OnLoaded { get; }
    }
    public struct HeaderVariable<T>
    {
        public int Group { get; set; }
        public string Name { get; set; }
        public T Value { get; set; }
    }
    internal enum ReaderState
    {
        None = 0,
        Group = 1,
        Name = 2,
        Code = 3
    }
    internal interface IReaderBuffer
    {
        void Push((string, string) value);
    }
    abstract class EntityObject
    {
        public string Handle { get; set; }
    }
    class Line : EntityObject
    {

    }
    internal class EntityReaderBuffer : IReaderBuffer
    {
        bool _handled;
        Line line = new Line();
        Func<Line, string, bool> ReadHandle = (line, value) => { line.Handle = value; return true; };
        Func<Line, string, bool> ApplicationDefinedGroup = (line, value) => { return true; };
        Func<Line, string,bool> fn;
        public void Push((string, string) value)
        {
            if (value.Item1 == "5" )
            {
                fn = ReadHandle;
                _handled = false;
            }else if (value.Item1 == "102" && fn!=ApplicationDefinedGroup)
            {
                fn = ApplicationDefinedGroup;
                _handled = false;
            }else if(value.Item1=="330" && fn == ApplicationDefinedGroup)
            {

            }
            else {
               _handled= fn(line, value.Item2);
                
            }
        }

        
    }
    internal class HeaderReaderBuffer:IReaderBuffer
    {
        private Dictionary<string, DxfHeaderValue> _headerValue = new Dictionary<string, DxfHeaderValue>();
        internal string Current;
        private ReaderState _state = ReaderState.None;
        int _groupCode;
   

        public void Push((string, string) value)
        {
#if DEBUG
            //if ( (value.Item2== "$INSBASE" || Current == "$INSBASE") && Debugger.IsAttached)
              //  Debugger.Break();
#endif
            if (value.Item1 ==Utilities.GroupCodeVariableName)
            {
                _headerValue[value.Item2] = new DxfHeaderValue() { Name = value.Item2 };
                Current = value.Item2;
            }
            else {
                _headerValue[Current].GroupCodes.Add( Int32.Parse( value.Item1));
                _headerValue[Current].Values.Add(value.Item2);
                
            }
        }
        internal Dictionary<string, DxfHeaderValue> HeaderValues => _headerValue;
    }
    public class DxfReader : IDxfReader
    {
        ISubject<IDxfDocument> _onLoaded = new Subject<IDxfDocument>();
        bool _headerLoaded, _classesLoaded, _entitiesLoaded;
        public DxfReader(IFileReadLine lineReader, IDxfDocument document)
        {
            Check.NotNull(lineReader, nameof(IFileReadLine));
            Check.NotNull(document, nameof(IDxfDocument));
            this.LineReader = lineReader;
            this.Document = document;
        }
        protected IFileReadLine LineReader { get; }
        public IDxfDocument Document { get; }

        public IObservable<IDxfDocument> OnLoaded => _onLoaded.AsObservable();

        private void SendLoaded()
        {
            if (_headerLoaded && _classesLoaded && _entitiesLoaded)
                _onLoaded.OnNext(Document);
        }
        public void Load(string filename)
        {
            ISubject<string> lines = new ReplaySubject<string>();
            LineReader.Filename = filename;
            LineReader.Output.Subscribe(lines);
            var headerObs = lines.SkipWhile(s => s != "HEADER").Skip(1).TakeWhile(s => s != "ENDSEC");
            headerObs
                .Scan(("", ""), (acc, curent) => (acc.Item2, curent.Trim()))
                .Skip(1)
                .Where((e, i) => i % 2 == 0)
                .Aggregate(
               new HeaderReaderBuffer(),
               (buffer, value) =>
               {
                   buffer.Push(value);

                   return buffer;
               })
           .Subscribe(list =>
           {
               foreach (var key in list.HeaderValues.Keys)
               {
                   Document.Header.SetValue(key, list.HeaderValues[key]);
               }
           }, () =>
           {
               _headerLoaded = true;
               SendLoaded();
           });
            var classesObs = lines.SkipWhile(s => s != "CLASSES").TakeWhile(s => s != "ENDSEC");
            classesObs.Subscribe(l =>
            {
                //   Console.WriteLine(l);
            }, () =>
            {
                _classesLoaded = true;
                SendLoaded();
            });
            var entitiesObs = lines.SkipWhile(s => s != "ENTITIES").TakeWhile(s => s != "ENDSEC");
            entitiesObs
                .Scan(("","" ),(acc,curent)=>(acc.Item2,curent.Trim() ))
                .Skip(2)
                .Where((e,i)=>i%2==0)
                .Aggregate(new EntityReaderBuffer(),
               (buffer, value) =>
               {
                  // buffer.Push(value);

                   return buffer;
               })
                .Subscribe(l =>
            {
                   Console.WriteLine(l);
            }, () =>
            {
                _entitiesLoaded = true;
                SendLoaded();
            });
            //return res;
            //headerObs.Subscribe();
            //return Observable.Never<string>();
        }
    }
}
