using UnityEngine;

namespace Game.Core.Unity
{
    public interface IRandom
    {
        int Range(int start, int end);
    }

    public class UnityRandom : IRandom
    {
        public int Range(int start, int end)
        {
            return Random.Range(start, end);
        }
    }
}