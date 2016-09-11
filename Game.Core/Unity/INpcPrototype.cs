using System;
using UnityEngine;

namespace Game.Core.Unity
{
    public interface INpcPrototype
    {
        Npc Create();
    }
}