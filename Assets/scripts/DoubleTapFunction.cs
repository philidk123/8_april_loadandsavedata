using UnityEngine;

public class DoubleTapDetector : MonoBehaviour
{
    public float doubleTapTime = 0.3f;

    private float lastTapTime = 0f;

    public PlayerShield playerShield;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    // Double tap detected
                    playerShield.ActivateShield();
                }

                lastTapTime = Time.time;
            }
        }
    }
}