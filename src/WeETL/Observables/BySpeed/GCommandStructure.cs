namespace WeETL.Observables.BySpeed
{
    public struct GCommandStructure
    {
        public int? Line { get; set; }
        public int? Code { get; set; }
        public int? A { get; set; }
        public double? C { get; set; }
        public int? H { get; set; }
        public double? I { get; set; }
        public double? J { get; set; }
        public int? L { get; set; }
        public int[] M { get; set; }
        //public int? M { get; set; }
        public int? P { get; set; }
        public double? R { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public string Comment { get; set; }
        public override string ToString()
        => $"N->{Line}, G->{Code}, X->{X}, Y->{Y}, L->{L}, P->{P}, H->{H}, A->{A}";
    }
}
