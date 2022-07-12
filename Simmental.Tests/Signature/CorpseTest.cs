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
    internal class CorpseTest
    {
        [Test]
        public void CorpseSerialization()
        {
            string signature = "Bob (xx), stinky";
            var sp = new SignatureParts(signature);
            var xx = new Corpse(sp);

            Assert.AreEqual(signature, xx.GetSignature());
        }
    }
}
