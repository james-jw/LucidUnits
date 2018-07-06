using System;
using System.Collections.Generic;
using System.Text;

namespace LucidUnits.Units
{
    /// <summary>
    /// A unit of rotation. 
    /// </summary>
    public class UnitRotation : UnitValue
    {
        public static new string Unit { get => "ROTATION"; }

        static UnitRotation()
        {
            UnitValue.Register<UnitRotation>(Unit, UnitDegree.Unit,
                r => r * 360,
                d => d / 360
            );
        }

        /// <summary>
        /// Creates a unit of rotation with the provided value.
        /// </summary>
        /// <param name="value">The number of rotations.</param>
        public UnitRotation(double value) : base(Unit, value) { }
    }
}
