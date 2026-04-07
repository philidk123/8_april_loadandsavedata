using UnityEngine;
public abstract class BaseEnemy : MonoBehaviour
{
    protected float gravityScale = 1f;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }
}
