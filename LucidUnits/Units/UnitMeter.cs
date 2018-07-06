namespace LucidUnits
{

    public class UnitMeter : UnitValue
    {
        public new static string Unit { get => "METER"; }


        static UnitMeter()
        {
            UnitValue.Register<UnitMeter>(Unit, UnitCentimeter.Unit,
                m => m * 100,
                cm => cm / 100
            );
        }

        public UnitMeter(double value) : base(UnitMeter.Unit)
        {
            Value = value;
        }
    }
}
