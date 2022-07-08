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
    }
}
