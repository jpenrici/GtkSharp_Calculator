using NUnit.Framework;
using System;

namespace TestCalculatorGui
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TextView_Empty()
        {
            CalculatorGui.Calc t = new CalculatorGui.Calc("");
            Assert.AreEqual("ERROR", t.Result);
        }

        [Test]
        public void TextView_Simple()
        {
            CalculatorGui.Calc t = new CalculatorGui.Calc("((10 + (2 * 8)) - 6)");
            Assert.AreEqual("20,0", t.Result);
        }

        [Test]
        public void TextView_OtherParentheses()
        {
            CalculatorGui.Calc t = new CalculatorGui.Calc("{[10 + (2 * 8)] - 6}");
            Assert.AreEqual("20,0", t.Result);
        }

        [Test]
        public void TextView_NewLine()
        {
            CalculatorGui.Calc t = new CalculatorGui.Calc("{[10\n+\n(2*8)]\n-\n6}");
            Assert.AreEqual("20,0", t.Result);
        }
    }
}
