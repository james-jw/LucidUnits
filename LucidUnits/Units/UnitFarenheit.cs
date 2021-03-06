﻿namespace LucidUnits
{
    public class UnitFarenheit : UnitValue
    {
        public new static string Unit { get => "FAHRENHEIT"; }


        static UnitFarenheit()
        {
            UnitValue.Register<UnitFarenheit>(Unit, UnitCelsius.Unit,
                f => (f - 32d) * (5d / 9d),
                c => (c * 1.8d) + 32d
            );
        }

        public UnitFarenheit(double value) : base(Unit, value) { }
    }
}
