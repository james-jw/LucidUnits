using LucidUnits;
using System.Collections.Generic;

namespace LucidUnits
{

    public class UnitInch : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitInch>("INCH", UnitFoot.Unit,
            f => f * 12,
            i => i / 12
        );

        public UnitInch(double value) : base(UnitInch.Unit)
        {
            Value = value;
        }

   }
}
