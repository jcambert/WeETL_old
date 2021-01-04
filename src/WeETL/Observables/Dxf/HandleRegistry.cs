using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf
{
    public interface IHandleRegistry
    {
        List<IDxfObject> AddDocument(IDxfDocument document);
        void AssignHandle(IDxfDocument document, IDxfObject o);
    }
    public class HandleRegistry : IHandleRegistry
    {
        Dictionary<IDxfDocument, List<IDxfObject>> _registry = new Dictionary<IDxfDocument, List<IDxfObject>>();
        public List<IDxfObject> AddDocument(IDxfDocument document)
        {
            Check.NotNull(document, nameof(document));
            if (!_registry.ContainsKey(document))
                _registry.TryAdd(document, new List<IDxfObject>());
            return _registry[document];
        }
        public void AssignHandle(IDxfDocument document,IDxfObject o)
        {
            AddDocument(document).SetHandle(o).Add(o);
        }
    }

    internal static class HandleRegistryExtension
    {
        internal static List<IDxfObject> SetHandle(this List<IDxfObject> l,IDxfObject o)
        {
            if(o is DxfObject)
            {
                (o as DxfObject).Handle = (l.Count + 1).ToString();
            }
            return l;

        }
    }
}
