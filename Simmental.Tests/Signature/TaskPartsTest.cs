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
    public  class TaskPartsTest
    {
        [Test]
        public void TaskPartNoParams()
        {
            var task = new TaskParts("Wander");
            Assert.AreEqual("Wander", task.SignatureStamp);
            Assert.AreEqual(0, task.PartsCount);
        }

        [Test]
        public void TaskPartWithParams()
        {
            string signatureText = "Patrol [3,1]-[14,1]-[14,7]-[9,4], P2, P3";
            var sp = new TaskParts(signatureText);

            Assert.AreEqual("Patrol", sp.SignatureStamp);
            Assert.AreEqual("[3,1]-[14,1]-[14,7]-[9,4]", sp[0]);
            Assert.AreEqual("P2", sp[1]);
            Assert.AreEqual("P3", sp[2]);
        }

        [Test]
        public void TaskPartEdgeCaseParams()
        {
            string signatureText = "Patrol [3,1]-[14,1]-[14,7]-[9,4, P2, P3";
            var sp = new TaskParts(signatureText);

            Assert.AreEqual("Patrol", sp.SignatureStamp);
            Assert.AreEqual("[3,1]-[14,1]-[14,7]-[9,4, P2, P3", sp[0]);
            Assert.AreEqual(1, sp.PartsCount);
        }

        [Test]
        public void TaskPartLastParamBlank()
        {
            string signatureText = "Patrol ,,";
            var sp = new TaskParts(signatureText);

            Assert.AreEqual("Patrol", sp.SignatureStamp);
            Assert.AreEqual("", sp[0]);
            Assert.AreEqual("", sp[1]);
            Assert.AreEqual("", sp[2]);
            Assert.AreEqual(3, sp.PartsCount);
        }


    }
}
