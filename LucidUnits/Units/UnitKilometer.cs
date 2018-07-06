﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitKilometer : UnitValue
    {
        public static new string Unit { get => "KILOMETER"; }

        static UnitKilometer()
        {
            UnitValue.Register<UnitKilometer>(Unit, UnitMeter.Unit,
                km => km * 1000,
                m => m / 1000
            );
        }

        public UnitKilometer(double value) : base(UnitKilometer.Unit)
        {
            Value = value;
        }
    }
}
