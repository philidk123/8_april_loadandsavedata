using UnityEngine;

public class FastEnemy : BaseEnemy
{
    private void Awake()
    {
        gravityScale = 0.5f;
    }
}