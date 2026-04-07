using UnityEngine;

public class SlowEnemy : BaseEnemy
{
    private void Awake()
    {
        gravityScale = 2f;
    }

}
