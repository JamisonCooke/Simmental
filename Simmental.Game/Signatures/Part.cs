using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Signatures
{
    public class Part
    {
        public Part() : this("", null, null) { }

        public Part(string value) : this("", null, value) { }

        public Part(string name, Type type) : this(name, type, null) { }
        public Part(string name, Type type, string value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Value { get; set; }
    }
}
