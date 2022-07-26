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
    internal class LightSourceTest
    {
        [Test]
        public void LightSourceSerialization()
        {
            string signature = "Torch (l), just a torch, 5, 6, True";
            var sp = new SignatureParts(signature);
            var l = new LightSource(sp);

            Assert.AreEqual(signature, l.GetSignature());
        }

        [Test]
        [TestCase("Short Sword (l), it glows now, 2, 6, True", "")]
        [TestCase("Short Sword (l), it glows now, 2, 6, Ture", "IsLit: Ture must be True or False")]
        [TestCase("Short Sword (l), 2, 6, True", "Distance: True must be a number, IsLit: Missing value, IsLit:")]
        [TestCase("Short Sword (l), it glows now, -2, 6, True", "")]
        [TestCase("Short Sword (l), it glows now, 2lkl, 6.555, True", "Brightness: 2lkl must be a number, Distance: 6.555 must be a number")]
        public void BadLightSourceSignature(string signatureText, string expectedError)
        {
            var sf = new SignatureFactory();
            string errorMessage = sf.ValidateSignature(signatureText);
            string truncatedMessage = expectedError.Length == 0 ? errorMessage : errorMessage.Substring(0, expectedError.Length);

            Assert.AreEqual(expectedError, truncatedMessage);
        }
    }
}
