using NUnit.Framework;
using Simmental.Game.Characters.Tasks;
using Simmental.Game.Items;
using Simmental.Game.Signatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Simmental.Tests.Signature
{
    internal class WaitTest
    {
        [Test]
        public void WaitSerialization()
        {
            string signature = "Wait 10";
            var tp = new TaskParts(signature);
            var l = new Wait(tp);

            Assert.AreEqual(signature, l.GetSignature());
        }

        [Test]
        [TestCase("Sleep 5", "The first word must be one of these: AttackPlayer, Wait, Wander.")]
        [TestCase("Wait hold on", "WaitTurns: hold on must be a number")]
        [TestCase("Wait 5, 55, 555", "Too many paramaters. Unexpected: 55")]
        [TestCase("AttackPlayer", "")]

        public void BadLightSourceSignature(string signatureText, string expectedError)
        {
            var tf = new TaskFactory();
            string errorMessage = tf.ValidateSignature(signatureText);
            string truncatedMessage = expectedError.Length == 0 ? errorMessage : errorMessage.Substring(0, expectedError.Length);

            Assert.AreEqual(expectedError, truncatedMessage);
        }
    }
}
