using UnityEngine;

namespace Game.Core.Unity
{
    public interface IGui
    {
        string TextArea(Rect area, string text);
        string TextField(Rect area, string text);
        void Label(Rect area, string text, TextAnchor alignment, Color backgroundColor, float padding);

        float GetLabelHeight();
    }
}
