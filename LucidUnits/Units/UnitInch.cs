using LucidUnits;
using System.Collections.Generic;

namespace LucidUnits
{
    public class UnitInch : UnitValue
    {
        public static new string Unit { get => "INCH"; }

        static UnitInch()
        {
            UnitValue.Register<UnitInch>(Unit, UnitFoot.Unit,
                i => i / 12,
                f => f * 12
            );
        }

        public UnitInch(double value) : base(Unit)
        {
            Value = value;
        }
   }
}
