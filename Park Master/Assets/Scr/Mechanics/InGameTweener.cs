using System;
using DG.Tweening;
using UnityEngine;

namespace Scr.Mechanics
{
    public class InGameTweener : IDisposable
    {
        protected Sequence _sequence;
        public float PermanentTimeScale => _permanentTimeScale;
        private float _permanentTimeScale = 1f;
        public bool IsPlaying => _sequence != null ? _sequence.IsPlaying() : false;
        
        protected virtual void InitSequence()
        {
            Stop();
            _sequence = DOTween.Sequence();
            SetTimescaleForCurrent(_permanentTimeScale);
        }
        
        
        public Sequence GetNewSequence()
        {
            var sequence = DOTween.Sequence();
            sequence.timeScale = PermanentTimeScale;
            return sequence;
        }
        
        public virtual void Stop(bool complete = false)
        {
            if (_sequence != null)
            {
                _sequence.Kill(complete);
                _sequence = null;
            }
        }
        
        public virtual void SetTimescaleForCurrent(float timeScale)
        {
            if (_sequence != null)
            {
                _sequence.timeScale = timeScale;
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}