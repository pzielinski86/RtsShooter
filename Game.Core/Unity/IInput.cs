using UnityEngine;

namespace Game.Core.Unity
{
    public interface IInput
    {
        bool GetKey(KeyCode keyCode);
        float GetScrollWheelDelta();
        float GetMouseXDelta();
        float GetMouseYDelta();

        bool GetLeftMouseButton();
        bool GetRightMouseButton();
        bool IsKeyDown(KeyCode key);
    }

    public class UnityInput:IInput
    {
        public bool GetKey(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }

        public float GetScrollWheelDelta()
        {
            return Input.GetAxis("Mouse ScrollWheel");
        }

        public float GetMouseXDelta()
        {
            return Input.GetAxis("Mouse X");
        }

        public float GetMouseYDelta()
        {
            return Input.GetAxis("Mouse Y");
        }

        public bool GetLeftMouseButton()
        {
            return Input.GetMouseButton(0);
        }

        public bool GetRightMouseButton()
        {
            return Input.GetMouseButton(1);
        }

        public bool IsKeyDown(KeyCode key)
        {
            return Event.current.type == EventType.keyDown && Event.current.keyCode == key;
        }
    }
}