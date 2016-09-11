using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Core.Gun
{
    public abstract class GunBase
    {
        private DateTime _lastUsed;
        protected GunBase(BulletsHandler bulletsHandler)
        {
            BulletsHandler = bulletsHandler;
            CoolDownTimeInMs = 500;
            Range = 25;
        }

        public BulletsHandler BulletsHandler { get; private set; }
        public int CoolDownTimeInMs { get; private set; }
        public uint Range { get; private set; }

        public void Shoot(Vector3 startPos, Vector3 direction)
        {
            if ((DateTime.Now - _lastUsed).TotalMilliseconds > CoolDownTimeInMs)
            {
                _lastUsed = DateTime.Now;
                DoShoot(startPos, direction);
            }
        }

        protected abstract void DoShoot(Vector3 startPos, Vector3 direction);
    }
}