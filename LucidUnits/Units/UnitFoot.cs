namespace LucidUnits
{
    public class UnitFoot : UnitValue
    {
        public new static string Unit { get; } = UnitValue.Register<UnitFoot>("FOOT", UnitMeter.Unit,
            m => m * 3.28084d,
            f => f / 3.28084d
        ); 

        public UnitFoot(double value) : base(Unit)
        {
            Value = value;
        }
    }
}
