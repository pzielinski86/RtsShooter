using NUnit.Framework;
using Game.Core.Controls.CommandsLine.Commands;

namespace Game.Core.Tests.Controls.CommandsLine
{
    [TestFixture]
    public class DisplayTextTests
    {
        [Test]
        public void When_CommandTextIsValid_And_ParameterContainsSingleWord_ThenItShouldBeLoaded()
        {
            var displayText = new DisplayText(null);
            bool isLoaded = displayText.TryToLoadFromText("display test");

            Assert.That(isLoaded,Is.True);
            Assert.That(displayText.Text,Is.EqualTo("test"));
        }

        [Test]
        public void When_CommandTextIsValid_And_ParameterContainsTwoWords_ThenItShouldBeLoaded()
        {
            var displayText = new DisplayText(null);
            bool isLoaded = displayText.TryToLoadFromText("display test test2");

            Assert.That(isLoaded, Is.True);
            Assert.That(displayText.Text, Is.EqualTo("test test2"));
        }

        [Test]
        public void When_CommandTextIsValid_And_ParameterContainsQuotedSentence_ThenSentenceShouldBeLoaded()
        {
            var displayText = new DisplayText(null);
            bool isLoaded = displayText.TryToLoadFromText("display \"test test2\"");

            Assert.That(isLoaded, Is.True);
            Assert.That(displayText.Text, Is.EqualTo("test test2"));
        }

        [Test]
        public void When_CommandTextIsInvalid_Then_ItShouldNotBeLoaded()
        {
            var displayText = new DisplayText(null);
            bool isLoaded = displayText.TryToLoadFromText("displdgday1 \"test test2\"");

            Assert.That(isLoaded, Is.False);
            Assert.That(displayText.Text,Is.Empty);
        }
    }
}
