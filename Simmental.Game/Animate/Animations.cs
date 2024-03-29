﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Interfaces;

namespace Simmental.Game.Animate
{
    [Serializable]
    public class Animations : IAnimations
    {
        private List<IAnimation> _animationList = new List<IAnimation>();

        public IAnimation DefaultAnimation { get; set; }

        public IAnimation Current
        {
            get
            {
                

                if (_animationList.Count == 0)
                    return DefaultAnimation;
                else
                    return _animationList[0];
            }
        }

        public void Clear() => _animationList.Clear();
        public void AddFirst(IAnimation animation) => _animationList.Insert(0, animation);
        public void Add(IAnimation animation) => _animationList.Add(animation);

        public List<IAnimation> ExpireAnimations()
        {
            var result = new List<IAnimation>();

            _animationList.RemoveAll(a => {
                if (a is null) return true; 
                if (DateTime.Now > a.StartTime + a.Duration)
                {
                    result.Add(a);
                    return true;
                }

                return false;
            });
            return result;
        }

    }
}
