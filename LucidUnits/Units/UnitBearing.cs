using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits
{
    public class UnitBearing : UnitValue
    {
        public static new string Unit { get; } = UnitValue.Register<UnitBearing>("BEARING", UnitDegree.Unit,
            bearing => {
                var degree = (360 - bearing) + 90;
                while (degree >= 360)
                    degree -= 360;

                return degree;
            },
            degrees => {
                var bearing = (360 - degrees) + 90;
                while (bearing >= 360)
                    bearing -= 360;

                return bearing;
            }
        );

        public UnitBearing(double value) : base(Unit)
        {
            Value = value;
        }
    }
}
