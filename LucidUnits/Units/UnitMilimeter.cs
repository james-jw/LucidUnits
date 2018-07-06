using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits.Units
{
    public class UnitMillimeter : UnitValue
    {
        public static new string Unit { get => "MILLIMETER"; }

        static UnitMillimeter()
        {
            UnitValue.Register<UnitMillimeter>(Unit, UnitMeter.Unit,
                mm => mm / 1000,
                m => m * 1000
            );
        } 

        public UnitMillimeter(double value) : base(Unit, value) { }
    }
}
