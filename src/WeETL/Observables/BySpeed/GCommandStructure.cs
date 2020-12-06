namespace WeETL.Observables.BySpeed
{
    public struct GCommandStructure
    {
        public int? Line { get; set; }
        public int? Code { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public int? L { get; set; }
        public int? P { get; set; }
        public int? H { get; set; }
        public int? A { get; set; }

        public override string ToString()
        => $"N->{Line}, G->{Code}, X->{X}, Y->{Y}, L->{L}, P->{P}, H->{H}, A->{A}";
    }
}
