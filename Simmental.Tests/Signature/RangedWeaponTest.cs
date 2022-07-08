using NUnit.Framework;
using Simmental.Game.Items;
using Simmental.Game.Signatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Tests.Signature
{
    internal class RangedWeaponTest
    {
        [Test]
        public void RangedWeaponSerialization()
        {
            string signature = "Arrow (rw), Grisly Arrow, 12, 5, Fire, bolt";
            var sp = new SignatureParts(signature);
            var rw = new RangedWeapon(sp);

            Assert.AreEqual(signature, rw.GetSignature());
        }
    }
}
