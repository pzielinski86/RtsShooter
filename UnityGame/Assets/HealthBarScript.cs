using UnityEngine;
using System.Collections;
using Game.Core;

public class HealthBarScript : MonoBehaviour {

    private WorldMap _worldMap;
    
    private void Start()
    {
        _worldMap = GetComponent<WorldMapScript>().WorldMap;

    }

    private void OnGUI()
    {
        const int height = 17;
        const int margin = 10;
        const int width = 120;

        var texture = CreateBarTexture(width, height);

        int startPosX = Screen.width-width-margin;

        GUI.Box(new Rect(startPosX, margin, width, height), texture, GUIStyle.none);
    }

    private Texture2D CreateBarTexture(int width, int height)
    {
        int greenMax = (int)( (float)_worldMap.Player.CurrentHealth / _worldMap.Player.MaxHealth * width);
        var texture = new Texture2D(width, height);

        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                SetHealthPixel(greenMax, texture, i, j);
            }
        }

        texture.Apply();

        return texture;
    }

    private static void SetHealthPixel(int maxGreenOffset, Texture2D texture, int x, int y)
    {
        const float green = 0.85f;

        if (x < maxGreenOffset)
            texture.SetPixel(x, y, new Color(0, green, 0f));
        else
            texture.SetPixel(x, y, Color.gray);
    }
}
