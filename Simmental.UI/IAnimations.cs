using System.Collections.Generic;

namespace Simmental.Interfaces
{
    public interface IAnimations
    {
        IAnimation Current { get; }
        IAnimation DefaultAnimation { get; set; }
        List<IAnimation> ExpireAnimations();

        void Add(IAnimation animation);
        void AddFirst(IAnimation animation);
        void Clear();
    }
}