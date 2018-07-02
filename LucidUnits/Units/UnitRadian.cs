using System;

namespace LucidUnits
{
    public class UnitRadian : UnitValue
    {
        public new static string Unit { get; } = "RADIAN";

        public UnitRadian(double value) : base(UnitRadian.Unit)
        {
            Value = value; 
        }
    }
}
