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

            //var sig2 = sp.ToSignature();
            Assert.AreEqual(signature, mw.GetSignature());

        }
        [Test]
        public void MeleeErrorMessage()
        {
            string signatureText = "Short Sword (mw), Rusty sword you picked up somewhere";
            string expectedError = "DamageRoll: Missing value";

            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);

            Assert.AreEqual(expectedError, errorMessage);
        }

        [Test]
        public void MeleeErrorBadDamageRoll()
        {
            // Should be 2d8, not 2r8
            string signatureText = "Short Sword (mw), Rusty sword you picked up somewhere, 2r8";
            string expectedError = "'2r8' is missing a d.";

            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);

            Assert.AreEqual(expectedError, errorMessage);
        }

        [Test]
        public void BadStamp()
        {
            // notice it's (wm) not (mw)
            string signatureText = "Short Sword (wm), Rusty sword you picked up somewhere";
            string expectedErrorStart = "Invalid item stamp 'wm'.";

            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);

            Assert.AreEqual(expectedErrorStart, errorMessage.Substring(0, expectedErrorStart.Length));
        }

        [Test]
        public void MissingStamp()
        {
            // notice we removed (mw)
            string signatureText = "Short Sword, Rusty sword you picked up somewhere";
            string expectedErrorStart = "Missing item stamp.";

            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);

            Assert.AreEqual(expectedErrorStart, errorMessage.Substring(0, expectedErrorStart.Length));
        }

        [Test]
        public void MissingStampAndSignatureText()
        {
            // notice we removed (mw)
            string signatureText = ", Rusty Sword";
            string expectedErrorStart = "Missing item stamp.";

            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);

            Assert.AreEqual(expectedErrorStart, errorMessage.Substring(0, expectedErrorStart.Length));
        }

        [Test]
        public void StampParensBackwards()
        {
            // notice it's (wm) not (mw)
            string signatureText = "Short Sword )wm(, Rusty sword you picked up somewhere";
            string expectedErrorStart = "Missing item stamp";

            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);

            Assert.AreEqual(expectedErrorStart, errorMessage.Substring(0, expectedErrorStart.Length));
        }

        [Test]
        public void StampParensHalfDorked()
        {
            // notice it's (wm) not (mw)
            string signatureText = "Short Sword )wm, Rusty sword you picked up somewhere";
            string expectedErrorStart = "Missing item stamp";

            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);

            Assert.AreEqual(expectedErrorStart, errorMessage.Substring(0, expectedErrorStart.Length));
        }


    }
}
