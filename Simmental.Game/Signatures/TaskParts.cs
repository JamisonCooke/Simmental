using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Signatures
{
    public class TaskParts : SignatureParts
    {
        public TaskParts(string signatureText) : base(signatureText)
        {
        }

        public TaskParts(Type type, params string[] textParts) : base(type, textParts)
        {

        }
    }
}
