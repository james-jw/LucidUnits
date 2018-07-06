using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitMile : UnitValue
    {
        public static new string Unit { get => "MILE"; }

        static UnitMile()
        {
            UnitValue.Register<UnitMile>(Unit, UnitFoot.Unit,
                m => m * 5280,
                f => f / 5280
            );
        }

        public UnitMile(double value) : base(Unit)
        {
            Value = value;
        }
    }
}
