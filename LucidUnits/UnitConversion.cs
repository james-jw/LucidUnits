using System;

namespace LucidUnits
{
    public class UnitConversion
    {
        public UnitConversion(string from, string too, Func<double, double> conversion)
        {
            From = from;
            Too = too;
            Convert = conversion;
        }

        public string Id { get => $"{From}_{Too}"; }

        public string From { get; set; }
        public string Too { get; set; }
        public Func<double, double> Convert { get; set; }
    }
}
