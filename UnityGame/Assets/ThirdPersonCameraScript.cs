using UnityEngine;
using System.Collections;
using Game.Core;
using Game.Core.Math;
using Game.Core.Properties;
using Game.Core.Unity;

public class ThirdPersonCameraScript : MonoBehaviour
{
    public GameObject PlayerObject;
    public Color CrosshairColor = Color.white;

    private ThirdPersonCamera _camera;
    private PlayerScript _playerScript;
    private GUIStyle _lineStyle;


    private float bend;
    // Use this for initialization
    void Start()
    {
        _playerScript = PlayerObject.GetComponent<PlayerScript>();

        _camera = new ThirdPersonCamera(new UnityTransform(transform), new UnityInput());

        CreateCrosshairStyle();
    }

    // Update is called once per frame

    void Update()
    {
        _camera.Update(_playerScript.Player);
    }


    void OnGUI()
    {
        if (_playerScript.Player.State == CharacterState.Aim)
        {
            Vector3 centerPoint = new Vector3(Screen.width / 2f, Screen.height / 2f);

            const float crossHairSize = 15;
            const float spread = 5f;
            const float thickness = 3f;
            
            var topMiddleRect = new Rect(centerPoint.x - thickness / 2, centerPoint.y - (crossHairSize + spread), thickness,
                crossHairSize);
            var middleBottomRect = new Rect(centerPoint.x - thickness / 2, centerPoint.y + spread, thickness, crossHairSize);
            var middleRightRect = new Rect(centerPoint.x + spread, (centerPoint.y - thickness / 2), crossHairSize, thickness);
            var leftMiddleRect = new Rect(centerPoint.x - (crossHairSize + spread), (centerPoint.y - thickness / 2), crossHairSize,
                thickness);

            GUI.Box(topMiddleRect, "", _lineStyle);
            GUI.Box(middleBottomRect, "", _lineStyle);
            GUI.Box(middleRightRect, "", _lineStyle);
            GUI.Box(leftMiddleRect, "", _lineStyle);
        }
    }

    private void CreateCrosshairStyle()
    {        
        var tex = new Texture2D(1, 1);
        SetTextureColor(tex, CrosshairColor);
        _lineStyle = new GUIStyle();
        _lineStyle.normal.background = tex;
    }

    private void SetTextureColor(Texture2D texture, Color color)
    {
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, color);
            }
        }

        texture.Apply();
    }
}
