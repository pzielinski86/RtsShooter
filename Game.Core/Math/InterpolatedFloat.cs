using System;
using UnityEngine;

namespace Game.Core.Math
{
    public class InterpolatedFloat : InterpolatedDataBase<float>
    {
        public InterpolatedFloat() : base( Mathf.Lerp)
        {
        }
    }

    public class InterpolatedVector3 : InterpolatedDataBase<Vector3>
    {
        public InterpolatedVector3() : base(Vector3.Lerp)
        {
        }
    }

    public abstract class InterpolatedDataBase<T>
    {
        private readonly Func<T, T,float, T> _interpolate;
        public T Current { get; private set; }
        public bool Completed { get; private set; }

        private T _start;
        private T _end;
        private float _startTimeInSeconds;
        private float _interpolationTimeInSeconds;

        protected InterpolatedDataBase(Func<T,T,float,T> interpolate)
        {
            _interpolate = interpolate;
            Completed = true;
        }

        public void Set(T value)
        {
            _start = _end = Current = value;
            Completed = true;
        }

        public void Reset(T start, T end, float startTimeInSeconds, float interpolationTimeInSeconds)
        {
            Completed = false;
            Current = start;
            _start = start;
            _end = end;
            _startTimeInSeconds = startTimeInSeconds;
            _interpolationTimeInSeconds = interpolationTimeInSeconds;
        }

        public void Update(float currentTimeInSecondsFromGameStart)
        {
            if (Completed)
                return;

            float progress = (currentTimeInSecondsFromGameStart - _startTimeInSeconds) / _interpolationTimeInSeconds;

            if (progress >= 1f)
                Completed = true;
            else
                Current = _interpolate(_start, _end, progress);
        }

        public void Revert(float time)
        {
            Completed = false;

            _end = _start;
            _start = Current;

            _interpolationTimeInSeconds = Mathf.Min(time - _startTimeInSeconds, _interpolationTimeInSeconds);
            _startTimeInSeconds = time;
        }
    }
}