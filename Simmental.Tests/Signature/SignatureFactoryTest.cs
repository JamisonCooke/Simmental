using System;
using NUnit.Framework;
using Simmental.Game.Items;
using Simmental.Game.Signatures;
using Simmental.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Tests.Signature
{
    internal class SignatureFactoryTest
    {

        [Test]
        public void TorchCreateNoType()
        {
            var sf = new SignatureFactory();

            string signature = "Torch (l), just a torch, 5, 6, True";
            ISignature s = sf.Create(signature);

            Assert.AreEqual(signature, s.GetSignature());
        }

        [Test]
        public void TorchCreate()
        {
            var sf = new SignatureFactory();

            string signature = "Torch (l), just a torch, 5, 6, True";
            var l = (LightSource)sf.Create(signature);

            Assert.AreEqual(signature, l.GetSignature());
        }

        [Test]
        public void MeleeWeaponCreate()
        {
            var sf = new SignatureFactory();

            string signature = "Short Sword (mw), Rusty sword you picked up somewhere, 2d8";
            var mw = (MeleeWeapon) sf.Create(signature);

            Assert.AreEqual(signature, mw.GetSignature());

        }

        [Test]
        [TestCase("Top Level", 0)]
        [TestCase("  First Level", 1)]
        [TestCase("    Second Level", 2)]
        // [TestCase(" Bad Level", 1)]
        public void IndentationCheck(string text, int depth)
        {
            var sf = new SignatureFactory();
            int foundDepth = sf.IndentationDepth(text);

            Assert.AreEqual(depth, foundDepth);
        }

        [Test]
        public void MultiCreateDepth2()
        {
            string signature = "Backpack (c), Leather Backpack\r\n" +
                "  Short Sword (mw), Rusty sword you picked up somewhere, 2d8";

            var sf = new SignatureFactory();
            var backpack  = (Container) sf.Create(signature);
            Assert.AreEqual(1, backpack.Count);

            Assert.AreEqual("Backpack (c), Leather Backpack", backpack.GetSignature());
            ISignature shortSword = (ISignature) backpack.Items.First();
            Assert.AreEqual("Short Sword (mw), Rusty sword you picked up somewhere, 2d8", shortSword.GetSignature());

            string multiLineSignature = SignatureFactory.GetMultilineSignature(backpack);
            Assert.AreEqual(signature, multiLineSignature);

        }

        [Test]
        public void MultiCreateDepth3()
        {
            string signature = "Bobpack (xx), Leather Backpack\r\n" +
                "  Box (c), Cheap cardboard box\r\n" +
                "    Short Sword (mw), Rusty sword you picked up somewhere, 2d8";

            var sf = new SignatureFactory();
            var backpack = (Container)sf.Create(signature);

            string multiLineSignature = SignatureFactory.GetMultilineSignature(backpack);
            Assert.AreEqual(signature, multiLineSignature);
        }

        [Test]
        public void MultiCreateDepth4()
        {
            string signature = 
                "Backpack (c), Leather Backpack\r\n" +
                "  Box (c), Cheap cardboard box\r\n" +
                "    Short Sword (mw), Rusty sword you picked up somewhere, 2d8\r\n" +
                "    Long Sword (mw), Fancy sword, 2d8\r\n" +
                "  Russian Doll (c), Red russian doll\r\n" +
                "    Nested Russian Doll (c), doll\r\n" +
                "      Another Nested Russian Doll (c), doll\r\n" +
                "        Dagger Sword (mw), What are those russians hiding?, 2d8\r\n" +
                "    Excessive Dust (c), Dustin in there" +
                "";

            var sf = new SignatureFactory();
            var backpack = (Container)sf.Create(signature);

            string multiLineSignature = SignatureFactory.GetMultilineSignature(backpack);
            Assert.AreEqual(signature, multiLineSignature);
        }

        [Test]
        public void CreateMultipleFromSignatures()
        {
            var sf = new SignatureFactory();
            
            // Signatures like you'd find in the designer-- 
            string signature = "Sword (mw), Rusty, 2d8\r\nBox (c), Ruby covered box\r\n  Sword2 (mw), Rusty, 2d8\r\nSword3 (mw), Rusty, 2d8";
            var items = sf.CreateMultiple(signature);

            var f = items.First();

            string newSig = SignatureFactory.GetMultilineSignature(items);

            Assert.AreEqual(signature, newSig);
        }

    }
}
