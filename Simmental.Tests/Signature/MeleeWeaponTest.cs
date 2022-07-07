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
    internal class MeleeWeaponTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MeleeCreate() 
        {
            string signature = "Short Sword (mw), Rusty sword you picked up somewhere, 2d8";
            var sp = new SignatureParts(signature);
            var mw = new MeleeWeapon(sp);

            Assert.AreEqual("Short Sword", mw.Name);
            Assert.AreEqual("2d8", mw.DamageRoll.GetRollDescription());

            var sig2 = sp.ToSignature();
            Assert.AreEqual(signature, sig2);

        }
    }
}
