using System;

namespace LucidUnits
{
    public class UnitDegree : UnitValue
    {
        public new static string Unit { get => "DEGREE"; }

        static UnitDegree()
        {
            UnitValue.Register<UnitDegree>(Unit, UnitRadian.Unit,
                (d) => d * (Math.PI / 180d),
                (r) => r * (180d / Math.PI)
            );
        }

        public UnitDegree(double value) : base(Unit, value) { }
    }
}
