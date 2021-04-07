using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeETL.DependencyInjection;

namespace WeETL.Observables.GCode
{
    public static class Extensions
    {
        internal static double? TrySetDoubleValue(this string from) => double.TryParse(from, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double res) ? res : null;

        internal static int? TrySetIntValue(this string from) => Int32.TryParse(from, out int res) ? res : null;

        internal static int[] TrySetIntMultipleValue(this Group col)
        {
            if (col.Captures.Count == 0) return null;
            int[] res = new int[col.Captures.Count];
            for (int i = 0; i < col.Captures.Count; i++)
            {
                int v = TrySetIntValue(col.Captures[i].Value) ?? 0;
                res[i] = v;

            }
            return res;
        }
        static string CommentCleanPattern = "[_=\"()]";
        internal static string TrySetComment(this Group col)
        {
            string res = col?.Value ?? string.Empty;
            return Regex.Replace(res, CommentCleanPattern, "", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        }

        static GPattern Pattern = new GPattern();

        private static readonly Func<string, GCommandLine> _CommandParser = (line) =>
        {

            return Pattern.Parse(line);

            /*string pattern = @"^(N(?<line>\d+))+(G(?<code>\d+))*(X(?<x>\d*(\.\d+)*))*(Y(?<y>\d*(\.\d+)*))*(Z(?<z>\d*(\.\d+)*))*(L(?<l>\d+))*(P(?<p>\d+))*(H(?<h>\d+))*(A(?<a>\d+))*(H(?<h>\d+))*(I(?<i>\d*(\.\d+)*))*(J(?<j>\d*(\.\d+)*))*(R(?<r>\d*(\.\d+)*))*(M(?<m>\d*(\.\d+)*))*(C(?<c>\d*(\.\d+)*))*(?<comment>\([\s\S]*?\))*";
            Regex Regex = new Regex(pattern);
            try
            {
                var l = TrySetIntValue(Regex.Match(line).Groups["line"].Value);
                if (l == null) return new GCommandLine() { N = l };
                GCommandLine result = new GCommandLine
                {
                    N = l,
                    Code = TrySetIntValue(Regex.Match(line).Groups["code"]?.Value),
                    X = TrySetDoubleValue(Regex.Match(line).Groups["x"]?.Value),
                    Y = TrySetDoubleValue(Regex.Match(line).Groups["y"]?.Value),
                    Z = TrySetDoubleValue(Regex.Match(line).Groups["z"]?.Value),
                    A = TrySetIntValue(Regex.Match(line).Groups["a"]?.Value),
                    B = TrySetIntValue(Regex.Match(line).Groups["b"]?.Value),
                    C = TrySetIntValue(Regex.Match(line).Groups["c"]?.Value),
                    I = TrySetDoubleValue(Regex.Match(line).Groups["i"]?.Value),
                    J = TrySetDoubleValue(Regex.Match(line).Groups["j"]?.Value),
                    K = TrySetDoubleValue(Regex.Match(line).Groups["k"]?.Value),
                    R = TrySetDoubleValue(Regex.Match(line).Groups["r"]?.Value),
                    L = TrySetIntValue(Regex.Match(line).Groups["l"]?.Value),
                    M = TrySetIntMultipleValue(Regex.Match(line).Groups["m"]),
                    P = TrySetIntValue(Regex.Match(line).Groups["p"]?.Value),
                    H = TrySetIntValue(Regex.Match(line).Groups["h"]?.Value),
                    Comment = TrySetComment(Regex.Match(line).Groups["comment"])
                };

                return result;
            }
            catch
            {
                return new GCommandLine() { N = -1 };
            }*/
        };

        internal static Func<string, GCommandLine> CommandParser => _CommandParser;

        public static IServiceCollection RegisterCommand(this IServiceCollection sc, Type type)
            => sc.AddTransient(typeof(IGCommand), type);
        public static IServiceCollection RegisterCommand<T>(this IServiceCollection sc)
            where T : class, IGCommand
       => sc.AddTransient<IGCommand, T>();

        public static TCommand ResolveCommand<TCommand>(this ServiceProvider sp)
            where TCommand : class, IGCommand
        => sp.ResolveCommand(typeof(TCommand).Name) as TCommand;

        public static IGCommand ResolveCommand(this ServiceProvider sp, string name)
        => sp.ResolveKeyed<IGCommand>(name);

        public static IServiceCollection UseGCommands(this IServiceCollection sc)
        {
            sc.AddTransient<IGProgram, GProgram>();
            var types = typeof(Extensions).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsPublic && typeof(IGCommand).IsAssignableFrom(t));
            foreach (var type in types)
            {
                sc.RegisterCommand(@type);
            }

            return sc;
        }
    }
}
