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

        [Test]
        [TestCase("Arrow (rw), Grisly Arrow, 12, 5, Fire, bolt", "")]
        [TestCase("Arrow (rw), Grisly Arrow, 12, 5, Wire, bolt", "Element: Wire must be a damage type, ie: Fire, Lightning, Ice")]
        public void BadRangedWeaponSignature(string signatureText, string expectedError)
        {
            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);
            string truncatedMessage = expectedError.Length == 0 ? errorMessage : errorMessage.Substring(0, expectedError.Length);

            Assert.AreEqual(expectedError, truncatedMessage);
        }
    }
}
