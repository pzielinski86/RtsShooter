using UnityEngine;

namespace Game.Core.Unity
{
    public class UnityGuiAdapter : IGui
    {
        public string TextArea(Rect area, string text)
        {

            return GUI.TextArea(area, text);
        }

        public string TextField(Rect area, string text)
        {
            return GUI.TextField(area, text);
        }

        public void Label(Rect area, string text, TextAnchor alignment, Color backgroundColor, float padding)
        {
            var style = GetLabelStyle(alignment, backgroundColor);
            style.padding = new RectOffset((int)padding, (int)padding, (int)padding, (int)padding);

            GUI.Label(area, text, style);
        }     

        public float GetLabelHeight()
        {
            var style = GetLabelStyle(TextAnchor.UpperLeft, Color.black);          

            return style.CalcHeight(new GUIContent("W"), 100);
        }

        private static GUIStyle GetLabelStyle(TextAnchor alignment, Color backgroundColor)
        {
            var style = new GUIStyle { alignment = alignment };

            var background = new Texture2D(2, 2);

            background.SetPixels(new[] { backgroundColor, backgroundColor, backgroundColor, backgroundColor });
            background.Apply();
            style.normal.background = background;
            style.alignment = TextAnchor.UpperLeft;
            style.normal.textColor = Color.white;
            return style;
        }
    }
}