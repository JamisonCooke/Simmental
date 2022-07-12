using System;
using NUnit.Framework;
using Simmental.Game.Items;
using Simmental.Game.Signatures;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Tests.Signature
{
    internal class ContainerTest
    {
        [Test]
        [TestCase("Box (c), old and found on the sidewalk")]
        [TestCase("Backpack (c), Crappy red backpack from middle school")]
        public void ContainerSerialization(string signature)
        {
            //string signature = "Box (c), old and found on the sidewalk";
            var sp = new SignatureParts(signature);
            var c = new Container(sp);

            Assert.AreEqual(signature, c.GetSignature());
        }
    }
}
