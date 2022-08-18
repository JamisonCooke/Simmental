using NUnit.Framework;
using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Tests.Signature
{
    internal class PositionTest
    {
        [Test]
        public void PositionFromString()
        {
            var position = new Position("[4,5]");
            Assert.AreEqual(4, position.i);
            Assert.AreEqual(5, position.j);
        }
        [Test]
        public void PositionToString()
        {
            var position = new Position(4,5);
            string positionText = position.ToString();
            Assert.AreEqual("[4,5]", positionText);
        }
        [Test]
        [TestCase("[1,2]", "")]
        [TestCase("0,2", "")]
        [TestCase("e, 2", "Unable to create position object from string: invalid input e")]
        [TestCase("1,2]", "")]
        [TestCase("", "Position in wrong format ''. Expecting [i,j]")]
        [TestCase("hi where am I? :)", "Position in wrong format 'hi where am I? :)'. Expecting [i,j]")]
        [TestCase("[1,2,4]", "Position in wrong format '[1,2,4]'. Expecting [i,j]")]
        [TestCase("[1]", "Position in wrong format '[1]'. Expecting [i,j]")]
        public void PositionValidation(string positionText, string errormessage)
        {
            string returnedMessage = Position.IsValid(positionText);
            Assert.AreEqual(errormessage, returnedMessage);

        }
    }
}
