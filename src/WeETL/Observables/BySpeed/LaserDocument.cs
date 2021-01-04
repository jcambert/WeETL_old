using System.Collections.Generic;
using WeETL.IO;
using WeETL.Observables.BySpeed.Entities;

namespace WeETL.Observables.BySpeed
{
    public interface ILaserDocument : IDocument
    {
        Format Format { get; set; }

        void AddOrigins(int l, double x, double y,double c);

        List<(double, double,double)> this[int index] { get; }

        bool AddPiece(int? l, double? x, double? y, string comment, out int current);
        Dictionary<int, List<(double, double,double)>> Origins { get; }
        Dictionary<int, Piece> Pieces { get; }


    }
    public class LaserDocument : ILaserDocument
    {
        public Dictionary<int, List<(double, double,double)>> Origins { get; } = new Dictionary<int, List<(double, double,double)>>();
        public Dictionary<int, Piece> Pieces { get; } = new Dictionary<int, Piece>();

        public List<(double, double,double)> this[int index] => Origins[index];

        public Format Format { get; set; } = new Format();

        public void AddOrigins(int l, double x, double y,double c)
        {
            
            if (!Origins.ContainsKey(l))
                Origins[l] = new List<(double, double,double)>();
            Origins[l].Add((x, y,c));
        }

        public bool AddPiece(int? l, double? x, double? y, string comment, out int current)
        {
            current = l ?? -1;
            if (l == null || x == null || y == null) return false;
            Pieces[l.Value] = new Piece(x.Value, y.Value, comment);
            return true;
        }
    }
}
