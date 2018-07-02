namespace LucidUnits
{
    public class UnitMeter : UnitValue
    {
        public new static string Unit { get; } = UnitValue.Register<UnitMeter>("METER", UnitCentimeter.Unit,
            cm => cm / 100,
            m => m * 100
        );

        public UnitMeter(double value) : base(UnitMeter.Unit)
        {
            Value = value;
        }
    }
}
