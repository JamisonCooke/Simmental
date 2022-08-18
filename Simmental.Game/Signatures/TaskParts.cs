using Simmental.Game.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Signatures
{
    public class TaskParts : PartsBase
    {
        public TaskParts(string signatureText) : base(signatureText)
        {
        }

        public TaskParts(Type type, params string[] textParts) : base(type, textParts)
        {

        }

        public override void InitializeFromSignature(string signatureText)
        {
            _parts = new List<Part>();

            //           1         2         3         4
            // 0 2345678 0 2345678 0 2345678 0 2345678 0 2345678 
            // Patrol [3,1]-[14,1]-[14,7]-[9,4], P2, P3
            // <-SS-> <-------Params------------------>
            // Set _parts[] & SignatureStamp

            int firstSpace = signatureText.IndexOf(" ");
            if (firstSpace == -1)
            {
                SignatureStamp = signatureText;
                return;
            }

            SignatureStamp = signatureText.Substring(0, firstSpace);
            string partText = signatureText.Substring(firstSpace + 1);

            int lastComma = -1;
            var partIndices = FindParamaterSplitIndices(partText);
            foreach (int nextComma in partIndices)
            {
                _parts.Add(new Part(partText.Substring(lastComma + 1, nextComma - lastComma - 1).Trim()));
                lastComma = nextComma;
            }                
        }

        private List<int> FindParamaterSplitIndices(string partText)
        {
            // 0 2345678 1 2345678 2 2345678 3 2345678 0
            // [3,1]-[14,1]-[14,7]-[9,4], P2, P3

            List<int> parts = new();

            bool inBrackets = false;
            for(int i = 0; i < partText.Length; i++)
            {
                if (partText[i] == '[')
                    inBrackets = true;
                else if (partText[i] == ']')
                    inBrackets = false;
                else if (inBrackets == false && partText[i] == ',') 
                    parts.Add(i);
            }
            parts.Add(partText.Length);
            return parts;
        }

        /// <summary>
        /// Returns the full signature based on the SignatureStamp and the _parts[]
        /// </summary>
        /// <returns></returns>
        public override string ToSignature()
        {
            StringBuilder sb = new StringBuilder();

            // Always get the name w/ the signature stamp
            sb.Append($"{SignatureStamp}");

            // Loop over the rest of the paramters and comma delimit them on the end
            bool firstTime = true;
            foreach (Part part in _parts)
            {
                if (firstTime)
                {
                    firstTime = false;
                    sb.Append(" ");
                }
                else
                {
                    sb.Append(", ");
                }                
                sb.Append(part.Value);
            }
            return sb.ToString();
        }

        public override string StampFromType(Type type)
        {
            return TaskFactory.StampFromType(type);
        }

        public override Type TypeFromStamp(string signatureStamp)
        {
            return TaskFactory.TypeFromStamp(signatureStamp);
        }
    }
}
