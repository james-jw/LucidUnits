using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitYard : UnitValue
    {
        public static new string Unit { get => "YARD"; }

        static UnitYard()
        {
            UnitValue.Register<UnitYard>(Unit, UnitFoot.Unit,
                y => y * 3,
                f => f / 3
            );
        }

        public UnitYard(double value) : base(UnitYard.Unit)
        {
            Value = value;
        }

    }
}
