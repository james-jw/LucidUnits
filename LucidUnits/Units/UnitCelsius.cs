namespace LucidUnits
{
    public class UnitCelsius : UnitValue
    {
        public new static string Unit { get; } = UnitValue.Register<UnitCelsius>("CELSIUS"); 
        public UnitCelsius(double value) : base(UnitCelsius.Unit)
        {
            Value = value;
        }
    }
}
