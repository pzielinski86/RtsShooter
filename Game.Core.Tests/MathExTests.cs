using Game.Core.Math;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Game.Core.Tests
{
    [TestFixture]
    public class MathExTests
    {
        [TestCase(5, 5, 5, 5)]
        [TestCase(5, 3, 10, 5)]
        [TestCase(5, 6, 10, 6)]
        [TestCase(50, 6, 10, 10)]
        public void ClampTest(float value, float min, float max, float expectedOutput)
        {
            var output = MathEx.Clamp(value, min, max);

            Assert.That(output, Is.EqualTo(expectedOutput));
        }

    }
}