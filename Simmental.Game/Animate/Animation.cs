using Simmental.Game.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Interfaces;

namespace Simmental.Game.Animate
{
    [Serializable]
    public class Animation : IAnimation
    {
        public GraphicNameEnum GraphicName { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int[] SlideNos { get; set; }
        public TimeSpan SlideDuration { get; set; }
        public Position StartPosition { get; set; }
        public Position EndPosition { get; set; }
        public int CameraI { get; set; }
        public int CameraJ { get; set; }

        public IDisposable CacheObject { get; set; }



        public int GetSlideNo(DateTime time)
        {
            if (time < StartTime || (Duration != TimeSpan.MaxValue && time > StartTime + Duration))
                return -1;

            long slideNo = (long)((time - StartTime).TotalMilliseconds / SlideDuration.TotalMilliseconds);

            return SlideNos[slideNo % SlideNos.Length];
        }

        public double PercentComplete(DateTime time)
        {
            return (time - StartTime).TotalMilliseconds / Duration.TotalMilliseconds;
        }

        public Animation() { }
        public Animation(IGame game, GraphicNameEnum graphicName, DateTime startTime, TimeSpan duration, int[] slideNos, TimeSpan slideDuration, Position startPosition = null, Position endPosition = null)
        {
            CameraI = game.Wayfinder.CameraI;
            CameraJ = game.Wayfinder.CameraJ;

            GraphicName = graphicName;
            StartTime = startTime;
            Duration = duration;
            SlideNos = slideNos;
            SlideDuration = slideDuration;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }
}
