using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitYard : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitYard>("YARD", UnitFoot.Unit,
            y => y * 3,
            f => f / 3
        );

        public UnitYard(double value) : base(UnitYard.Unit)
        {
            Value = value;
        }

    }
}
