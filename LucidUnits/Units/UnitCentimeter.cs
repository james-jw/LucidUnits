using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitCentimeter : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitCentimeter>("CENTIMETER", UnitInch.Unit,
            cm => cm / 2.54d,
            i => i * 2.54d
        );

        public UnitCentimeter(double value) : base(Unit)
        {
            Value = value;
        }
    }
}
