using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits.Units
{
    public class UnitMillimeter : UnitValue
    {
        public static new string Unit { get; } = "MILLIMETER";

        public UnitMillimeter(double value) : base(UnitMillimeter.Unit)
        {
            Value = value;
        }
    }
}
