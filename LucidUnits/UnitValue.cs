using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LucidUnits
{

    /// <summary>
    /// Base class representing a unit value. 
    /// </summary>
    public class UnitValue
    {
        static Dictionary<string, UnitConversion> _converters = new Dictionary<string, UnitConversion>();

        static UnitValue()
        {
            foreach (var assembly in new Assembly[] { Assembly.GetExecutingAssembly(), Assembly.GetCallingAssembly() }.Distinct()) 
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.BaseType == typeof(UnitValue))
                    {
                        RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                    }
                }
            }
        }

        /// <summary>
        /// Registers a derived `UnitValue` type with the system. This should be called in the new unit types static constructor.
        /// 
        /// For example, to register a new type "DOZEN" with the system simply define the new unit class like so:
        /// <code>
        /// public class UnitDozen : UnitValue
        /// {
        ///    public static new Unit { get => "DOZEN" }
        ///    
        ///    static UnitDozen() 
        ///    {
        ///       UnitValue.Register&lt;UnitDozen&gt;(Unit, UnitValue.Any, 
        ///          d => d * 12,
        ///          n => n / 12
        ///       );
        ///    }
        ///    
        ///    public UnitDozen(double value) : base(Unit) 
        ///    {
        ///       Value = value;
        ///    }
        /// }
        /// </code>
        /// </summary>
        /// <typeparam name="T">The unit type to register</typeparam>
        /// <param name="unitName">The unit name</param>
        /// <param name="baseUnit">The base unit this unit converts too.</param>
        /// <param name="conversionFrom">The conversion from the new unit to the base unit.</param>
        /// <param name="conversionTo">The conversion to the new unit from the base unit.</param>
        /// <returns>The `unitName` provided.</returns>
        public static string Register<T>(string unitName, string baseUnit, Func<double, double> conversionFrom, Func<double, double> conversionTo)
        {
            AddConversion(unitName, baseUnit, conversionFrom);
            AddConversion(baseUnit, unitName, conversionTo);

            return unitName;
        }

        public UnitValue(string unit)
        {
            Unit = unit;
        }

        [JsonConstructor]
        public UnitValue(string unit, double value) : this(unit)
        {
            Value = value;
        }

        public virtual string Unit { get; set; }

        public double Value { get; set; }

        /// <summary>
        /// Returns a new UnitValue equal to this UnitValue but in the unit provided.
        /// </summary>
        /// <param name="unit">The unit to convert to</param>
        /// <returns>The converted value</returns>
        public UnitValue ConvertTo(string unit)
        {
            if (Unit == unit)
                return this;

            var converterName = $"{Unit}_{unit}";

            UnitConversion conversion = FindConversion(Unit, unit);
            if(conversion != null)
            {
                return new UnitValue(unit, conversion.Convert(this.Value));
            }

            throw new Exception($"Failed to convert value '{Value}' of unit '{Unit}' to '{unit}'. No converter, '{converterName}', exists");
        }

        private UnitConversion FindConversion(string unitFrom, string unitTo, bool allowGeneration = true)
        {
            if(!_converters.TryGetValue($"{unitFrom}_{unitTo}", out UnitConversion conversion) && allowGeneration)
            {
                UnitConversion dynamicConversion = GenerateDynamicConversion(unitFrom, unitTo);
                if (dynamicConversion != null)
                    _converters.Add($"{unitFrom}_{unitTo}", dynamicConversion);

                conversion = dynamicConversion;
            }

            return conversion;
        }

        private UnitConversion GenerateDynamicConversion(string unitFrom, string unitToo, HashSet<string> processed = null) 
        {
            processed = processed ?? new HashSet<string>();

            var conversions = AvailableConversionsTo(unitFrom);
            var validConversion = conversions.FirstOrDefault(c => c.Too == unitToo);

            if(validConversion == null)
            {
                foreach(var conversion in conversions.Where(c => !processed.Contains(c.Id)))
                {
                    processed.Add(conversion.Id);
                    var dynamicConversion = FindConversion(conversion.Too, unitToo, false) ?? GenerateDynamicConversion(conversion.Too, unitToo, processed);
                    if (dynamicConversion != null)
                    {
                        validConversion = new UnitConversion(unitFrom, unitToo, v => {
                            //Debug.WriteLine($"Executing conversion {conversion.Id} of value '{v}'");
                            var vOut = conversion.Convert(v);
                            //Debug.WriteLine($"Executing conversion {dynamicConversion.Id} of value '{vOut}'");
                            return dynamicConversion.Convert(vOut);
                        });

                        break;
                    }
                }
            }

            return validConversion;
        }

        private IEnumerable<UnitConversion> AvailableConversionsTo(string unitFrom)
        {
            foreach(var conversion in _converters.Values)
            {
                if (conversion.From == unitFrom)
                    yield return conversion;
            }
        }

        private static void AddConversion(string unitFrom, string unitToo, Func<double, double> conversion)
        {
            if(_converters == null)
                _converters = new Dictionary<string, UnitConversion>();

            var id = $"{unitFrom}_{unitToo}";
            if(!_converters.ContainsKey(id))
                _converters.Add(id, new UnitConversion(unitFrom, unitToo, conversion));
        }

        private static UnitValue ProcessOperator(UnitValue a, UnitValue b, Func<UnitValue, UnitValue, double> operation)
        {
            b = Coerce(a, b);

            return new UnitValue(a.Unit) {
                Value = operation(a, b)
            };
        }

        public static UnitValue operator+ (UnitValue a, UnitValue b) {
            return ProcessOperator(a, b, (x, y) => x.Value + y.Value);
        }

        public static UnitValue operator- (UnitValue a, UnitValue b) {
            return ProcessOperator(a, b, (x, y) => x.Value - y.Value);
        }

        public static UnitValue operator/ (UnitValue a, UnitValue b) {
            return ProcessOperator(a, b, (x, y) => x.Value / y.Value);
        }

        private static UnitValue Coerce(UnitValue a, UnitValue b)
        {
            var same = a?.Unit == b?.Unit;
            if (!same)
            {
                b = a?.Unit == null ? null : b?.ConvertTo(a.Unit);
            }

            return b;
        }

        static bool Nearly(double thisValue, double otherValue, double precision = .0001d)
        {
            return otherValue <= thisValue + precision && otherValue > thisValue - precision;
        }

        public static bool operator== (UnitValue a, UnitValue b)
        {
            b = Coerce(a, b);
            return a?.Value == null | b?.Value == null ? false : Nearly(a.Value, b.Value);
        }

        public static bool operator!= (UnitValue a, UnitValue b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() + Unit.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is UnitValue otherValue)
                return this == otherValue;

            return false;
        }

        public static bool operator< (UnitValue a, UnitValue b)
        {
            b = Coerce(a, b);
            return a.Value < b.Value;
        }

        public static bool operator> (UnitValue a, UnitValue b)
        {
            b = Coerce(a, b);
            return a.Value > b.Value;
        }

        public static bool operator<= (UnitValue a, UnitValue b)
        {
            b = Coerce(a, b);
            return a.Value <= b.Value;
        }

        public static bool operator>= (UnitValue a, UnitValue b)
        {
            b = Coerce(a, b);
            return a.Value >= b.Value;
        }

        public static UnitValue operator* (UnitValue a, double b) {
            return new UnitValue(a.Unit)
            {
                Value = a.Value * b
            };
        }
        public static UnitValue operator/ (UnitValue a, double b) {
            return new UnitValue(a.Unit)
            {
                Value = a.Value / b
            };
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}
