using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits.Units
{
    public class UnitRotation : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitRotation>("ROTATION", UnitDegree.Unit,
            r => r * 360,
            d => d / 360
        );

        public UnitRotation(double value) : base(Unit, value) { }
    }
}
