using System;
using Game.Core.Collections;

namespace Game.Core.Gun
{
    public class BulletsHandler
    {
        public ObservableCollection<BulletBase> Bullets { get; private set; }

        public BulletsHandler()
        {
            Bullets=new ObservableCollection<BulletBase>();
        }

        public void Update()
        {
            foreach (var bullet in Bullets)
            {
                bullet.Update();
            }
        }
    }
}