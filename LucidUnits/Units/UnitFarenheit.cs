namespace LucidUnits
{
    public class UnitFarenheit : UnitValue
    {
        public new static string Unit { get; } = UnitValue.Register<UnitFarenheit>("FAHRENHEIT", UnitCelsius.Unit,
            f => (f - 32d) * (5d / 9d),
            c => (c* 1.8d) + 32d
        );

        public UnitFarenheit(double value) : base(Unit)
        {
            Value = value;
        }
    }
}
