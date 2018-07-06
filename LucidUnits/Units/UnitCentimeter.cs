namespace LucidUnits
{
    public class UnitCentimeter : UnitValue
    {
        public static new string Unit { get => "CENTIMETER"; }

        static UnitCentimeter()
        {
            UnitValue.Register<UnitCentimeter>(Unit, UnitInch.Unit,
                cm => cm / 2.54d,
                i => i * 2.54d
            );
        }

        public UnitCentimeter(double value) : base(Unit, value) { }
    }
}
