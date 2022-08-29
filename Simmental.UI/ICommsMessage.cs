using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{

    public enum CommsMediumEnum
    {
        Verbal = 1,
        HandGestures = 2,
        Telepathic = 3
    }
   

    public interface ICommsMessage
    {
        /// <summary>
        /// Type of communications determines what mediums the message will travel through
        /// </summary>
        public CommsMediumEnum Medium { get; }

        /// <summary>
        /// Determines distance communications will travel
        /// </summary>
        public int Volume { get; }

        /// <summary>
        /// List of characters that have heard the message. 
        /// </summary>
        public ISet<ICharacter> HeardAlready { get; }


    }
}
