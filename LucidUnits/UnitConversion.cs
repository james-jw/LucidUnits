using System;

namespace LucidUnits
{
    public class UnitConversion
    {
        public UnitConversion(string from, string too, Func<double, double> conversin)
        {
            From = from;
            Too = too;
            Convert = Convert;
            
        }

        public string Id { get => $"{From}_{Too}"; }

        public string From { get; set; }
        public string Too { get; set; }
        public Func<double, double> Convert { get; set; }
    }
}
