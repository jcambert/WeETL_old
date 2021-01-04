using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeETL.IO;
using WeETL.Numerics;
using WeETL.Observables.BySpeed.Entities;
using WeETL.Observables.Dxf.Units;
using WeETL.Utilities;

namespace WeETL.Observables.BySpeed.IO
{
    public interface ILaserCutBySpeedReader : IFileReader<ILaserDocument>
    {
        Func<string, GCommandStructure> CommandParser { get; }
        bool RemovePrimings { get; set; }
        #region IFileReader

        #endregion
    }
    public class LaserCutBySpeedReader : ILaserCutBySpeedReader
    {
        ISubject<ILaserDocument> _onLoaded = new Subject<ILaserDocument>();
        public LaserCutBySpeedReader(IGProgramm program, IFileReadLine lineReader, ILaserDocument document)
        {
            Check.NotNull(program, nameof(IGProgramm));
            Check.NotNull(lineReader, nameof(IFileReadLine));
            Check.NotNull(document, nameof(document));
            this.Program = program;
            this.LineReader = lineReader;
            this.Document = document;
        }
        public Func<string, GCommandStructure> CommandParser => Extensions.CommandParser;

        public IGProgramm Program { get; }
        protected IFileReadLine LineReader { get; }
        public ILaserDocument Document { get; private set; }

        public IObservable<ILaserDocument> OnLoaded => _onLoaded.AsObservable();
        public bool RemovePrimings { get; set; } = false;
        private Vector2 GetGoodCenter(Vector2 start, Vector2 end, ((double, double), (double, double)) centers, AngleDirection dir)
        {
            Vector2 center1 = new Vector2(centers.Item1.Item1, centers.Item1.Item2);
            Vector2 center2 = new Vector2(centers.Item2.Item1, centers.Item2.Item2);

            if (dir == AngleDirection.CW)
            {
                if (end.X >= start.X && end.Y >= start.Y) return center1.Y > center2.Y ? center2 : center1;
                if (end.X > start.X && end.Y < start.Y) return center1.Y > center2.Y ? center2 : center1;
                if (end.X <= start.X && end.Y <= start.Y) return center1.Y < center2.Y ? center2 : center1;
                if (end.X < start.X && end.Y > start.Y) return center1.Y < center2.Y ? center2 : center1;
                return Vector2.Zero;
            }
            else
            {
                if (end.X >= start.X && end.Y >= start.Y) return center1.Y < center2.Y ? center2 : center1;
                if (end.X >= start.X && end.Y <= start.Y) return center1.Y < center2.Y ? center2 : center1;
                if (end.X <= start.X && end.Y <= start.Y) return center1.Y > center2.Y ? center2 : center1;
                if (end.X <= start.X && end.Y >= start.Y) return center1.Y > center2.Y ? center2 : center1;
                return Vector2.Zero;
            }
            /*  var v1 = start - new Vector2(centers.Item1.Item1, centers.Item1.Item2);
              var v2 = end- new Vector2(centers.Item1.Item1, centers.Item1.Item2);
              var res1 = Vector2.AngleBetween(v1, v2, true);

              var v3 = start - new Vector2(centers.Item2.Item1, centers.Item2.Item2);
              var v4 = end - new Vector2(centers.Item2.Item1, centers.Item2.Item2);
              var res2= Vector2.AngleBetween(v3,v4, true);
              //if (dir == AngleDirection.CCW) return res1 > res2 ? new Vector2(centers.Item1.Item1, centers.Item1.Item2) : new Vector2(centers.Item2.Item1, centers.Item2.Item2);
             // return res1 < res2 ? new Vector2(centers.Item1.Item1, centers.Item1.Item2) : new Vector2(centers.Item2.Item1, centers.Item2.Item2);
             return dir==AngleDirection.CW? new Vector2(centers.Item1.Item1, centers.Item1.Item2) : new Vector2(centers.Item2.Item1, centers.Item2.Item2);
            */
        }
        public virtual void Load(string filename)
        {
            bool _isInPriming = false;
            int currentPiece = -1;

            double x = 0.0, y = 0.0;
            double? i = null, j = null, r = null;
            int code = 0;
            int counter = 0;
            bool addEnable = true;
            Vector2 start = Vector2.Zero;
            LineReader.Filename = filename;
            LineReader
                .Output
                .Select(CommandParser)
                .Where(cmdLine => cmdLine.Line != null/* && cmdLine.Line <= 1010*/)
                .Subscribe((line) =>
                {
                    /* if (line.M != null && line.M.Value == 4)
                         _isInPriming = true;
                     if (line.M != null && line.M.Value == 5)
                         _isInPriming = false;
                     */
                    //Console.WriteLine(line.Line+" "+ line.Comment);

                    x = line.X == null ? x : line.X.Value;
                    y = line.Y == null ? y : line.Y.Value;

                    code = line.Code == null ? code : line.Code.Value;
                    // if (line.Line >= 1011) Debugger.Break();
                    if (line.M != null && (line.M.Contains(3) || line.M.Contains(4)))
                    {
                        addEnable = true;
                        if (!(line.M.Contains(0) || line.M.Contains(5) || line.M.Contains(12) || line.M.Contains(14)))
                            _isInPriming = true;
                    }
                    switch (code)
                    {
                        case 0:
                            if (currentPiece > 0)
                            {
                                start = new Vector2(x, y);
                            }
                            break;
                        case 1:
                            if (currentPiece > 0)
                            {
                                if (RemovePrimings && _isInPriming)
                                {
                                    start = new Vector2(x, y);
                                    _isInPriming = false;
                                }
                                else
                                {
                                    var to = new Vector2(x, y);
                                    if (!addEnable)
                                        start = to;
                                    else
                                        start = Document.Pieces[currentPiece].AddLine(start, to);
                                }
                            }
                            break;
                        case 2:
                            ++counter;
                            //if (counter >= 29 && counter <= 31) Debugger.Break();
                            if (currentPiece > 0)
                            {
                                if (RemovePrimings && _isInPriming)
                                {
                                    start = new Vector2(x, y);
                                    _isInPriming = false;
                                }
                                else
                                {
                                    Vector2 center = Vector2.Zero;
                                    var to = new Vector2(x, y);
                                    if (!addEnable)
                                    {
                                        start = to;
                                    }
                                    else
                                    if (line.R != null || r != null)
                                    {
                                        r = line.R == null ? r.Value : line.R.Value;
                                        var centers = CalculateCenter(start, to, r.Value);

                                        center = GetGoodCenter(start, to, centers, AngleDirection.CW);// new Vector2(_center.Item1, _center.Item2);
                                        start = Document.Pieces[currentPiece].AddArc("G2", start, to, center, Dxf.Units.AngleDirection.CW);
                                        i = j = null;

                                    }
                                    else
                                    {
                                        i = i ?? 0.0;
                                        j = j ?? 0.0;
                                        i = line.I == null ? i : line.I.Value;
                                        j = line.J == null ? j : line.J.Value;
                                        center = new Vector2(i.Value, j.Value);
                                        start = Document.Pieces[currentPiece].AddArc("G2", start, to, center, Dxf.Units.AngleDirection.CW);
                                        r = null;
                                    }

                                }
                            }
                            break;
                        case 3:
                            if (currentPiece > 0)
                            {
                                if (RemovePrimings && _isInPriming)
                                {
                                    start = new Vector2(x, y);
                                    _isInPriming = false;
                                }
                                else
                                {
                                    Vector2 center = Vector2.Zero;
                                    var to = new Vector2(x, y);
                                    if (!addEnable)
                                    {
                                        start = to;
                                    }
                                    else
                                    if (line.R != null || r != null)
                                    {

                                        r = line.R == null ? r.Value : line.R.Value;
                                        var centers = CalculateCenter(start, to, r.Value);

                                        center = GetGoodCenter(start, to, centers, AngleDirection.CCW); //new Vector2(_center.Item1, _center.Item2);
                                        start = Document.Pieces[currentPiece].AddArc("G3", start, to, center, Dxf.Units.AngleDirection.CCW);
                                    }
                                    else
                                    {
                                        i = i ?? 0.0;
                                        j = j ?? 0.0;
                                        i = line.I == null ? i : line.I.Value;
                                        j = line.J == null ? j : line.J.Value;
                                        center = new Vector2(i.Value, j.Value);
                                        start = Document.Pieces[currentPiece].AddArc("G3", start, to, center, Dxf.Units.AngleDirection.CCW);
                                        r = null;
                                    }

                                }
                            }
                            break;
                        case 28:
                            if (Document.AddPiece(line.L, x, y, line.Comment, out currentPiece))
                            {
                                Console.WriteLine($"Add Piece {line.L} to {x},{y} {line.Comment}");
                                start = new Vector2(x, y);
                                _isInPriming = false;
                            }

                            break;
                        case 29:
                            Document.Format = Document.Format ?? new Format();
                            Document.Format.Length = x;
                            Document.Format.Width = y;
                            break;
                        case 40:
                            _isInPriming = false;
                            break;
                        case 41:
                            _isInPriming = false;
                            break;
                        case 52:
                            Console.WriteLine($"Add Origine Piece {line.L} to {x},{y}");
                            var c = line.C == null ? 0.0 : line.C.Value;
                            var l = line.L == null ? 1 : line.L.Value;
                            Document.AddOrigins(l, x, y, c);
                            break;
                        case 99:
                            break;
                        default:
                            Console.WriteLine($"Code:{code} is not handled");
                            break;
                    }
                    if (line.M != null && (line.M.Contains(0) || line.M.Contains(5)))
                        addEnable = false;
                },
                () => _onLoaded.OnNext(Document));
        }

        static ((double, double), (double, double)) CalculateCenter(Vector2 x, Vector2 y, double radius)
        {
            double yy = (y.Y - x.Y) == 0 ? 0.00001 : (y.Y - x.Y);
            double k = (x.X - y.X) / yy;
            double h = (Math.Pow(y.X, 2) - Math.Pow(x.X, 2) + Math.Pow(y.Y, 2) - Math.Pow(x.Y, 2)) / (2 * yy);
            double m = Math.Pow(k, 2) + 1;
            double n = (-2 * x.X) + (2 * k * h) - (2 * k * x.Y);
            double p = Math.Pow(x.X, 2) + Math.Pow(x.Y, 2) - (2 * h * x.Y) + Math.Pow(h, 2) - Math.Pow(radius, 2);
            double x1 = (-n + Math.Sqrt(Math.Pow(n, 2) - (4 * m * p))) / (2 * m);
            double x2 = (-n - Math.Sqrt(Math.Pow(n, 2) - (4 * m * p))) / (2 * m);

            double y1 = k * x1 + h;
            double y2 = k * x2 + h;

            return ((x1, y1), (x2, y2));
        }
    }
}
