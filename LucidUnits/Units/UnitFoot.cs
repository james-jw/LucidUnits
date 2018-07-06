namespace LucidUnits
{
    public class UnitFoot : UnitValue
    {
        public static new string Unit { get => "FOOT"; }

        static UnitFoot()
        {
            UnitValue.Register<UnitFoot>(Unit, UnitMeter.Unit,
                f => f * .3048d,
                m => m / .3048d
            );
        }

        public UnitFoot(double value) : base(Unit)
        {
            Value = value;
        }
    }
}
