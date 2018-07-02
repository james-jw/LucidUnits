using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitMile : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitMile>("MILE", UnitFoot.Unit,
            m => m * 5280,
            f => f / 5280
        );

        public UnitMile(double value) : base(UnitMile.Unit)
        {
            Value = value;
        }
    }
}
