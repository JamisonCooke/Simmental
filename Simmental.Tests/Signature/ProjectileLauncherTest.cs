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
    internal class ProjectileLauncherTest
    {
        [Test]
        public void ProjectileLauncherSerialization()
        {
            // public ProjectileLauncher(string name, string description,string rangedWeaponType, IDamageRoll damageRoll)
            string signature = "Crossbow (pl), Found it on the ground, bolt, 2d6L";
            var sp = new SignatureParts(signature);
            var pl = new ProjectileLauncher(sp);

            Assert.AreEqual(signature, pl.GetSignature());
        }

        [Test]
        [TestCase("Crossbow(pl), Found it on the ground, bolt, 2d6L", "")]
        [TestCase("Crossbow(pl), Found it on the ground,, 2d6L", "RangedWeaponType: Missing value")]
        public void BadRangedWeaponSignature(string signatureText, string expectedError)
        {
            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);
            string truncatedMessage = expectedError.Length == 0 ? errorMessage : errorMessage.Substring(0, expectedError.Length);

            Assert.AreEqual(expectedError, truncatedMessage);
        }
    }
}
