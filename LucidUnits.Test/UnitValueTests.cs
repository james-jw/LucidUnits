using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LucidUnits.Test 
{
    [TestClass]
    public class UnitValueTests
    {
        [TestMethod]
        public void UnitTestAddition()
        {
            var yards = new UnitYard(2) + new UnitFoot(6);
            Assert.AreEqual(4, yards.Value);

            var degree = new UnitDegree(20) + (new UnitRadian(Math.PI * .5d) - new UnitDegree(10));
            Assert.IsTrue(new UnitDegree(100) == degree);
        }

        [TestMethod]
        public void UnitValueMultiplication()
        {
            var degree = new UnitDegree(20) * 3;
            Assert.AreEqual(60, degree.Value);
        }

        [TestMethod]
        public void GreaterThanTesting()
        {
            Assert.IsTrue(new UnitValue("FOOT", 1) < new UnitValue("METRE", 20));
            Assert.IsTrue(new UnitMile(1) < new UnitYard(1761));
            Assert.IsTrue(new UnitFoot(24) > new UnitYard(7.666667d));
            Assert.IsTrue(new UnitRadian(Math.PI) < new UnitDegree(190));
        }

        [TestMethod]
        public void EqualityNullUnitTest()
        {
            Assert.IsFalse(new UnitValue(UnitMile.Unit, 1) == null);
            Assert.IsFalse(new UnitMile(1) == null);
            Assert.IsFalse(null == new UnitMile(1));
            Assert.AreNotEqual(new UnitValue(UnitMile.Unit, 1), null);
            Assert.AreNotEqual(new UnitMile(1), null);
            Assert.AreNotEqual(null, new UnitMile(1));
        }

        [TestMethod]

        public void EqualityUnitTest()
        {
            Assert.IsTrue(new UnitValue(UnitMile.Unit, 1) == new UnitValue(UnitYard.Unit, 1760));
            Assert.IsTrue(new UnitMile(1) == new UnitYard(1760));
            Assert.IsTrue(new UnitMile(2) == new UnitYard(1760) + (new UnitMile(2) / 2));
            Assert.AreEqual(new UnitMile(1), new UnitInch(63360));
            Assert.IsTrue(new UnitBearing(90) == new UnitDegree(0));
            Assert.IsTrue(new UnitFoot(23) == new UnitYard(7.6667d).ConvertTo(UnitFoot.Unit));
            Assert.AreEqual(new UnitMetre(.3048d), new UnitFoot(1));
            Assert.AreEqual(new UnitFoot(1), new UnitMetre(.3048d));
        }

        [TestMethod]
        public void UnitTestDynamicConversion()
        {
            var yards = new UnitYard(0) + new UnitMile(1);
            Assert.AreEqual(1760, yards.Value);

            var inches = new UnitInch(0) + new UnitMile(1);
            Assert.AreEqual(63360, inches.Value);

            var centimeters = new UnitCentimeter(0) + new UnitMile(1);
            Assert.AreEqual(160934, Convert.ToInt32(centimeters.Value));

            var nauticalMiles = new UnitInch(10000000).ConvertTo(UnitNauticalMile.Unit);
            Assert.AreEqual(137, Convert.ToInt32(nauticalMiles.Value));
        }

        [TestMethod]
        public void UnitTestDynamicConversionPerformance()
        {
            var nM = (UnitValue)new UnitNauticalMile(0);
            var jump = new UnitInch(1);
            for(int i = 0; i < 10000000; i++)
            {
                nM = nM + jump;
            }

            Assert.AreEqual(137, Convert.ToInt32(nM.Value));
        }
    }
}
