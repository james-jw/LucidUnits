using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitKilometer : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitKilometer>("KILOMETER", UnitMeter.Unit,
            km => km * 1000,
            m => m / 1000
        );

        public UnitKilometer(double value) : base(UnitKilometer.Unit)
        {
            Value = value;
        }
    }
}
