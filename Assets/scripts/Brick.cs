using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPos.y < 0f) // viewportPos.y > 1f
        {
            Destroy(gameObject);
        }


    }
}
