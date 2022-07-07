using System;
using NUnit.Framework;
using Simmental.Game.Items;
using Simmental.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Tests.Signature
{
    internal class DamageRollTest
    {
        [Test]
        [TestCase("2d6")]
        [TestCase("2d6 + 10d4L")]
        [TestCase("2d6 + 10d4L +5")]
        [TestCase("2d6 + 10d4L -2")]
        public void DamageRollSerialization(string damageRollDescription)
        {
            // string damageRollDescription = "2d6 + 10d4L +5";
            var damage = new DamageRoll(damageRollDescription);
            var damageDescription = damage.GetRollDescription();
            Assert.AreEqual(damageRollDescription, damageDescription);

        }
    }
}
