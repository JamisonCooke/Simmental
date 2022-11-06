using System;

namespace Simmental.Interfaces
{
    public interface IAnimation
    {
        TimeSpan Duration { get; set; }
        Position EndPosition { get; set; }
        GraphicNameEnum GraphicName { get; set; }
        TimeSpan SlideDuration { get; set; }
        int[] SlideNos { get; set; }
        Position StartPosition { get; set; }
        DateTime StartTime { get; set; }

        int GetSlideNo(DateTime time);

        double PercentComplete(DateTime time);
    }
}