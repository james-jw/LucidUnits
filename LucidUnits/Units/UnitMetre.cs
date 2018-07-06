namespace LucidUnits
{
    public class UnitMetre : UnitValue
    {
        public static new string Unit { get => "METRE"; }

        static UnitMetre()
        {
            UnitValue.Register<UnitMetre>(Unit, UnitMeter.Unit,
                m => m,
                m => m
            );
        }

        public UnitMetre(double value) : base(Unit, value) {}
    }
}
