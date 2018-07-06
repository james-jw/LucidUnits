namespace LucidUnits
{
    public class UnitCelsius : UnitValue
    {
        public new static string Unit { get => "CELSIUS"; }

        static UnitCelsius()
        {
            UnitValue.Register<UnitCelsius>(Unit, UnitFarenheit.Unit,
                c => (c * 1.8d) + 32d,
                f => (f - 32d) * (5d / 9d)
            );
        }

        public UnitCelsius(double value) : base(Unit, value) { }
    }
}
