using System;
using NUnit.Framework;
using Simmental.Game.Items;
using Simmental.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Tests.Signature
{
    internal class DamageRollTest
    {
        [Test]
        [TestCase("2d6")]
        [TestCase("2d6 + 10d4L")]
        [TestCase("2d6 + 10d4L +5")]
        [TestCase("2d6 + 10d4L -2")]
        public void DamageRollSerialization(string damageRollDescription)
        {
            // string damageRollDescription = "2d6 + 10d4L +5";
            var damage = new DamageRoll(damageRollDescription);
            var damageDescription = damage.GetRollDescription();
            Assert.AreEqual(damageRollDescription, damageDescription);

        }

        [Test]
        [TestCase("2d6F", "")]
        [TestCase("2b6F", "'2b6F' is missing a d.")]
        [TestCase("2d6f", "'6f' in '2d6f' needs to be a number.")]
        [TestCase("2d6F +2", "")]
        [TestCase("2d6F + 2", "'2' in '2d6F + 2' is invalid: '2' is missing a d.")]
        [TestCase("2d6F + 2d6L", "")]
        
        public void DamageRollValidation(string damageRollDescription, string expectedError)
        {

            
            string errorMessage = DamageRoll.ValidateDamageRoll(damageRollDescription);
            string truncatedMessage = expectedError.Length == 0 ? errorMessage : errorMessage.Substring(0, expectedError.Length);

            Assert.AreEqual(expectedError, truncatedMessage);
        }
    }
}
