using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Observables.Dxf
{

    public enum Section
    {
        Header,
        Classes,
        Tables,
        Blocks,
        Entities,
        Objects,
        ThumbnailImage
    }
    public interface IReaderFactory
    {

    }
    public interface IReader
    {
        void Read((int, string) code);
    }

    internal static class EntityGroupCode
    {
        public const int EntityType = 0;
        public const int Handle = 5;
    }
    internal static class EntityType
    {
        public const string Text = "TEXT";
        public const string Line = "Line";
        public const string Circle = "CIRCLE";
    }
    internal class EntitySectionTextReader : IReader
    {
        public void Read((int, string) code)
        {

        }
    }
    internal class EntitySectionLineReader : IReader
    {
        public void Read((int, string) code)
        {

        }
    }
    internal class EntitySectionCircleReader : IReader
    {
        public void Read((int, string) code)
        {

        }
    }
    public class EntitySectionReader : IReader
    {
        IReader currentReader;
        public void Read((int, string) code)
        {
            if (code.Item1 == EntityGroupCode.EntityType)
            {
                currentReader = code.Item2 switch
                {
                    EntityType.Text => new EntitySectionTextReader(),
                    EntityType.Line=>new EntitySectionLineReader(),
                    EntityType.Circle=> new EntitySectionCircleReader(),
                    _ => null
                };
            }
            else
            {
                _ = currentReader ?? throw new Exception("Malformed dxf");

                currentReader.Read(code);

            }
        }
    }

    public class ReaderFactory : IReaderFactory
    {
        public IReader Create(Section section)
        {
            return default(IReader);

        }
    }
}
