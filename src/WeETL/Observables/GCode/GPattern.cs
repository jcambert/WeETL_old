using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeETL.Observables.GCode
{
    internal enum GPatternType
    {
        INTEGER = 0,
        MULTI_INTEGER,
        DECIMAL,
        STRING
    }
    internal class GCommandLinePatternDefinition
    {
        private PropertyInfo _setter;

        private GCommandLinePatternDefinition(bool required, bool startWith) => (Required, StartWith) = (required, startWith);

        public GCommandLinePatternDefinition(string header, bool required, bool startWith)
            : this(header, header.ToLowerInvariant(), required, startWith)
        { }
        public GCommandLinePatternDefinition(string header, string group, bool required, bool startWith)
        {
            this.Header = header;
            this.Group = group;
            this.Required = required;
            this.StartWith = startWith;

        }
        public GCommandLinePatternDefinition(string header, string group, Expression<Func<GCommandLine, int?>> e, bool required = false, bool startWith = false)
            : this(header, group, required, startWith)
        {
            this.PatternType = GPatternType.INTEGER;
            this.SetIntegerSetter(e);
        }
        public GCommandLinePatternDefinition(string header, string group, Expression<Func<GCommandLine, int[]>> e, bool required = false, bool startWith = false)
            : this(header, group, required, startWith)
        {
            this.PatternType = GPatternType.MULTI_INTEGER;
            this.SetMultiIntegerSetter(e);
        }
        public GCommandLinePatternDefinition(string header, string group, Expression<Func<GCommandLine, double?>> e, bool required = false, bool startWith = false)
            : this(header, group, required, startWith)
        {
            this.PatternType = GPatternType.DECIMAL;
            this.SetDoubleSetter(e);
        }
        public GCommandLinePatternDefinition(string header, string group, Expression<Func<GCommandLine, string>> e, bool required = false, bool startWith = false)
        : this(header, group, required, startWith)
        {
            this.PatternType = GPatternType.STRING;
            this.SetStringSetter(e);
        }
        public GCommandLinePatternDefinition(string header, Expression<Func<GCommandLine, int?>> e, bool required = false, bool startWith = false)
            : this(header, required, startWith)
        {
            this.PatternType = GPatternType.INTEGER;
            this.SetIntegerSetter(e);
        }
        public GCommandLinePatternDefinition(string header, Expression<Func<GCommandLine, int[]>> e, bool required = false, bool startWith = false)
            : this(header, required, startWith)
        {
            this.PatternType = GPatternType.INTEGER;
            this.SetMultiIntegerSetter(e);
        }
        public GCommandLinePatternDefinition(string header, Expression<Func<GCommandLine, double?>> e, bool required = false, bool startWith = false)
            : this(header, required, startWith)
        {
            this.PatternType = GPatternType.DECIMAL;
            this.SetDoubleSetter(e);
        }
        public GCommandLinePatternDefinition(string header, Expression<Func<GCommandLine, string>> e, bool required = false, bool startWith = false)
        : this(header, required, startWith)
        {
            this.PatternType = GPatternType.STRING;
            this.SetStringSetter(e);
        }
        public GCommandLinePatternDefinition(Expression<Func<GCommandLine, int?>> e, bool required = false, bool startWith = false)
            : this(required, startWith)
        {
            this.PatternType = GPatternType.INTEGER;
            this.SetIntegerSetter(e, true);
        }
        public GCommandLinePatternDefinition(Expression<Func<GCommandLine, int[]>> e, bool required = false, bool startWith = false)
            : this(required, startWith)
        {
            this.PatternType = GPatternType.MULTI_INTEGER;
            this.SetMultiIntegerSetter(e, true);
        }
        public GCommandLinePatternDefinition(Expression<Func<GCommandLine, double?>> e, bool required = false, bool startWith = false)
            : this(required, startWith)
        {
            this.PatternType = GPatternType.DECIMAL;
            this.SetDoubleSetter(e, true);
        }
        public GCommandLinePatternDefinition(Expression<Func<GCommandLine, string>> e, bool required = false, bool startWith = false)
        : this(required, startWith)
        {
            this.PatternType = GPatternType.STRING;
            this.SetStringSetter(e, true);
        }
        public GCommandLinePatternDefinition(string header, string group, GPatternType type, bool required = false, bool startWith = false)
        {
            this.Header = header;
            this.Group = group;
            this.PatternType = type;
            this.Required = required;
            this.StartWith = startWith;
        }

        private void SetIntegerSetter(Expression<Func<GCommandLine, int?>> memberLambda, bool setHeaderFromMember = false)
        {
            var memberSelectorExpression = memberLambda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                _setter = memberSelectorExpression.Member as PropertyInfo;
                if (setHeaderFromMember)
                {
                    Header = _setter.Name;
                    Group = Header.ToLowerInvariant();
                }
            }
        }
        private void SetMultiIntegerSetter(Expression<Func<GCommandLine, int[]>> memberLambda, bool setHeaderFromMember = false)
        {
            var memberSelectorExpression = memberLambda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                _setter = memberSelectorExpression.Member as PropertyInfo;
                if (setHeaderFromMember)
                {
                    Header = _setter.Name;
                    Group = Header.ToLowerInvariant();
                }
            }
        }

        private void SetDoubleSetter(Expression<Func<GCommandLine, double?>> memberLambda, bool setHeaderFromMember = false)
        {
            var memberSelectorExpression = memberLambda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                _setter = memberSelectorExpression.Member as PropertyInfo;
                if (setHeaderFromMember)
                {
                    Header = _setter.Name;
                    Group = Header.ToLowerInvariant();
                }
            }
        }
        private void SetStringSetter(Expression<Func<GCommandLine, string>> memberLambda, bool setHeaderFromMember = false)
        {
            var memberSelectorExpression = memberLambda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                _setter = memberSelectorExpression.Member as PropertyInfo;
                if (setHeaderFromMember)
                {
                    Header = _setter.Name;
                    Group = Header.ToLowerInvariant();
                }
            }
        }

        public string Header { get; private set; }
        public string Group { get; private set; }
        public GPatternType PatternType { get; }

        public bool Required { get; }
        public bool StartWith { get; }

        public override string ToString()
        {
            var res = $@"{GetStartWith()}({Header}(?<{Group}>{GetTypeFormated()})){GetRequiredFormated()}";
            return res;
        }


        private string GetTypeFormated()
            => PatternType switch
            {
                GPatternType.INTEGER => @"\d+",
                GPatternType.MULTI_INTEGER=> @"\d+",
                GPatternType.DECIMAL => @"\d*(\.\d+)*",
                GPatternType.STRING => @"\([\s\S]*?\)",
                _ => throw new Exception($"{PatternType} is not supported")
            };

        private string GetRequiredFormated() => Required ? "+" : "*";
        private string GetStartWith() => StartWith ? "^" : "";

        internal void SetValue(GCommandLine line, GroupCollection groups)
        {
            object _value = PatternType switch
            {
                GPatternType.INTEGER => Extensions.TrySetIntValue(groups[Group]?.Value),
                GPatternType.MULTI_INTEGER => Extensions.TrySetIntMultipleValue(groups[Group]),
                GPatternType.DECIMAL => Extensions.TrySetDoubleValue(groups[Group]?.Value),
                GPatternType.STRING => Extensions.TrySetComment(groups[Group]),
                _ => null
            };
            _setter?.GetSetMethod().Invoke(line, new[] { _value });
            //_setter?.SetValue(line,_value , null);
        }

    }
    internal class GPattern
    {
        private List<GCommandLinePatternDefinition> _patterns;
        private string _pattern;
        private Regex _regex;

        public GPattern()
        {
            _patterns = new List<GCommandLinePatternDefinition>()
            {
            new GCommandLinePatternDefinition("N","line",e=>e.N,true,true),
            new GCommandLinePatternDefinition("G","code",e=>e.Code,true),
            new GCommandLinePatternDefinition(e=>e.X),
            new GCommandLinePatternDefinition(e=>e.Y),
            new GCommandLinePatternDefinition(e=>e.Z),
            new GCommandLinePatternDefinition(e=>e.A),
            new GCommandLinePatternDefinition(e=>e.B),
            new GCommandLinePatternDefinition(e=>e.C),
            new GCommandLinePatternDefinition(e=>e.I),
            new GCommandLinePatternDefinition(e=>e.J),
            new GCommandLinePatternDefinition(e=>e.K),
            new GCommandLinePatternDefinition(e=>e.R),
            new GCommandLinePatternDefinition(e=>e.L),
            new GCommandLinePatternDefinition(e=>e.M),
            new GCommandLinePatternDefinition(e=>e.P),
            new GCommandLinePatternDefinition(e=>e.H),
            //new GCommandLinePatternDefinition("" ,e=>e.Comment),

            };
            _patterns.ForEach(p => _pattern += p.ToString());
            _regex = new Regex(_pattern);
        }
        //internal static string LINE = @"^(N(?<line>\d+))+";
        //string pattern = @"(G(?<code>\d+))*(X(?<x>\d*(\.\d+)*))*(Y(?<y>\d*(\.\d+)*))*(Z(?<z>\d*(\.\d+)*))*(L(?<l>\d+))*(P(?<p>\d+))*(H(?<h>\d+))*(A(?<a>\d+))*(H(?<h>\d+))*(I(?<i>\d*(\.\d+)*))*(J(?<j>\d*(\.\d+)*))*(R(?<r>\d*(\.\d+)*))*(M(?<m>\d*(\.\d+)*))*(C(?<c>\d*(\.\d+)*))*(?<comment>\([\s\S]*?\))*";

        internal GCommandLine Parse(string line)
        {
            GCommandLine result = new GCommandLine();
            Match _match = _regex.Match(line);
            
            if(_match.Success)
                _patterns.ForEach(p => p.SetValue(result ,_match.Groups)) ;
            return result;
        }
    }
}
