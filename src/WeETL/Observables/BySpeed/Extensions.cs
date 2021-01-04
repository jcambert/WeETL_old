using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using WeETL.DependencyInjection;
using WeETL.Observables.BySpeed.IO;

namespace WeETL.Observables.BySpeed
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
        static string CommentCleanPattern= "[_=\"()]";
        internal static string TrySetComment(this Group col)
        {
            string res = col?.Value??string.Empty;
            return Regex.Replace(res, CommentCleanPattern, "", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        }
        private static readonly Func<string, GCommandStructure> _CommandParser = (line) =>
        {
            //string pattern = @"^N(?<line>\d+)";
            //Console.WriteLine(line);
            string pattern = @"^(N(?<line>\d+))+(G(?<code>\d+))*(X(?<x>\d*(\.\d+)*))*(Y(?<y>\d*(\.\d+)*))*(L(?<l>\d+))*(P(?<p>\d+))*(H(?<h>\d+))*(A(?<a>\d+))*(H(?<h>\d+))*(I(?<i>\d*(\.\d+)*))*(J(?<j>\d*(\.\d+)*))*(R(?<r>\d*(\.\d+)*))*(M(?<m>\d*(\.\d+)*))*(C(?<c>\d*(\.\d+)*))*(?<comment>\([\s\S]*?\))*";
            Regex Regex = new Regex(pattern);
            try
            {
                var l = TrySetIntValue(Regex.Match(line).Groups["line"].Value);
               // if (l.Value == 1002) Debugger.Break();
                if (l == null) return new GCommandStructure() { Line = l };
                GCommandStructure result = new GCommandStructure
                {
                    Line = l,
                    Code = TrySetIntValue(Regex.Match(line).Groups["code"]?.Value),
                    C = TrySetDoubleValue(Regex.Match(line).Groups["c"]?.Value),
                    X = TrySetDoubleValue(Regex.Match(line).Groups["x"]?.Value),
                    Y = TrySetDoubleValue(Regex.Match(line).Groups["y"]?.Value),
                    I = TrySetDoubleValue(Regex.Match(line).Groups["i"]?.Value),
                    J = TrySetDoubleValue(Regex.Match(line).Groups["j"]?.Value),
                    R = TrySetDoubleValue(Regex.Match(line).Groups["r"]?.Value),
                    L = TrySetIntValue(Regex.Match(line).Groups["l"]?.Value),
                    M = TrySetIntMultipleValue(Regex.Match(line).Groups["m"]),
                    P = TrySetIntValue(Regex.Match(line).Groups["p"]?.Value),
                    H = TrySetIntValue(Regex.Match(line).Groups["h"]?.Value),
                    A = TrySetIntValue(Regex.Match(line).Groups["a"]?.Value),
                    Comment = TrySetComment(Regex.Match(line).Groups["comment"])
                };

                return result;
            }
            catch
            {
                return new GCommandStructure() { Line = -1 };
            }
        };

        internal static Func<string, GCommandStructure> CommandParser => _CommandParser;

        public static IServiceCollection RegisterCommand(this IServiceCollection sc, Type type)
            => sc.AddTransient(typeof(IGCommand), type);
        public static IServiceCollection RegisterCommand<T>(this IServiceCollection sc)
            where T : class, IGCommand
       => sc.AddTransient<IGCommand, T>();

        public static IServiceCollection UseGCommands(this IServiceCollection sc)
        {
            sc.AddTransient<IGProgramm, GProgram>();
            sc.AddTransient<ILaserCutBySpeedReader, LaserCutBySpeedReader>();
            sc.AddTransient<ILaserDocument, LaserDocument>();
            var types = typeof(Extensions).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsPublic && typeof(IGCommand).IsAssignableFrom(t) /* && t.IsTypeof< ETLComponent <NoneSchema,NoneSchema> > ( )*/);
            foreach (var type in types)
            {
                sc.RegisterCommand(@type);
            }

            return sc;
        }

        public static TCommand ResolveCommand<TCommand>(this ServiceProvider sp/*,GCommandStructure line=null*/)
            where TCommand : class, IGCommand
        => sp.ResolveCommand(typeof(TCommand).Name) as TCommand;

        public static IGCommand ResolveCommand(this ServiceProvider sp, string name/*,GCommandStructure line=null*/)
        => sp.ResolveKeyed<IGCommand>(name);



    }
}
