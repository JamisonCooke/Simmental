namespace Simmental.Interfaces
{
    public interface IAnimations
    {
        IAnimation Current { get; }
        IAnimation DefaultAnimation { get; set; }

        void Add(IAnimation animation);
        void AddFirst(IAnimation animation);
        void Clear();
    }
}