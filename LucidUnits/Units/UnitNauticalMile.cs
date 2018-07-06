using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitNauticalMile : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitNauticalMile>("NAUTICALMILE", UnitMile.Unit,
            nm => nm / .868976d,
            m => m * .868976d
        );

        public UnitNauticalMile(double value) : base(UnitNauticalMile.Unit)
        {
            Value = value;
        }
    }
}
