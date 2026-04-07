using UnityEngine;
using UnityEngine.InputSystem;

public class InputSys : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsPressing(out Vector2 screenPos)
    {

        screenPos = Vector2.zero;

        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            screenPos = Mouse.current.position.ReadValue();
            return true;
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        var accelerometer = Accelerometer.current;
        if (accelerometer != null)
        {
            var value = accelerometer.acceleration.value.x;
            if (value > 0.2)
            {
                screenPos = Camera.main.ViewportToScreenPoint(Vector2.one);
                return true;
            }

            if (value < -0.2)
            {
                screenPos = Camera.main.ViewportToScreenPoint(Vector2.zero);
                return true;
            }
        }

        return false;
    }
}
