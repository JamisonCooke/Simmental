using NUnit.Framework;
using Simmental.Game.Items;
using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Tests.Signature
{
    public class RollPartTest
    {
        [Test]
        public void RollPartSerialization()
        {
            var p1 = new RollPart("2d6");
            Assert.AreEqual(2, p1.NumberOfRolls);
            Assert.AreEqual(6, p1.DiceMax);
            Assert.AreEqual(ElementEnum.Normal, p1.Element);
            Assert.AreEqual("2d6", p1.ToString());

            var p2 = new RollPart("10d20F");
            Assert.AreEqual(10, p2.NumberOfRolls);
            Assert.AreEqual(20, p2.DiceMax);
            Assert.AreEqual(ElementEnum.Fire, p2.Element);
            Assert.AreEqual("10d20F", p2.ToString());
        }
    }
}
