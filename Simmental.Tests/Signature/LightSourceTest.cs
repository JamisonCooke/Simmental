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
    }
}
