using System;
using System.Collections.Generic;
using System.Linq;

namespace LucidUnits
{
    public class UnitValue
    {
        static Dictionary<string, UnitConversion> _converters;

        public static string Register<T>(string unitName, string baseUnit, Func<double, double> conversionFrom, Func<double, double> conversionTo)
        {
            AddConversion(unitName, baseUnit, conversionFrom);
            AddConversion(baseUnit, unitName, conversionTo);

            return unitName;
        }

        protected static string Register<T>(string unitName)
        {
            return unitName;
        }

        public UnitValue(string unit)
        {
            Unit = unit;
        }

        public UnitValue(string unit, double value) : this(unit)
        {
            Value = value;
        }
        public virtual string Unit { get; set; }

        public double Value { get; set; }

        public UnitValue ConvertTo(string unit)
        {
            if (Unit == unit)
                return this;

            var converterName = $"{Unit}_{unit}";

            Func<double, double> conversion = FindConversion(Unit, unit);
            if(conversion != null)
            {
                return new UnitValue(unit, conversion(this.Value));
            }

            throw new Exception($"Failed to convert value '{Value}' of unit '{Unit}' to '{unit}'. No converter, '{converterName}', exists");
        }

        private Func<double, double> FindConversion(string unitFrom, string unitTo)
        {
            if(!_converters.TryGetValue($"{unitFrom}_{unitTo}", out UnitConversion conversion))
            {
                UnitConversion dynamicConversion = GenerateDynamicConversion(unitFrom, unitTo);
                if (dynamicConversion != null)
                    _converters.Add($"{unitFrom}_{unitTo}", dynamicConversion);

                conversion = dynamicConversion;
            }

            return conversion.Convert;
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
                    var dynamicConversion = GenerateDynamicConversion(conversion.Too, unitToo, processed);
                    if (dynamicConversion != null)
                    {
                        validConversion = new UnitConversion(unitFrom, unitToo, v => dynamicConversion.Convert(conversion.Convert(v)));
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

            _converters.Add($"{unitFrom}_{unitToo}", new UnitConversion(unitFrom, unitToo, conversion));
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
            var same = a.Unit == b.Unit;
            if (!same)
                b = b.ConvertTo(a.Unit);

            return b;
        }

        static bool Nearly(double thisValue, double otherValue, double precision = .0001d)
        {
            return otherValue <= thisValue + precision && otherValue > thisValue - precision;
        }

        public static bool operator== (UnitValue a, UnitValue b)
        {
            b = Coerce(a, b);
            return Nearly(a.Value, b.Value);
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
            return obj is UnitValue uv && uv.Value == Value && uv.Unit == Unit;
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
