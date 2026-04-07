using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public DodgerAttributes dodgerAttributes;

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int startingScore = 0;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputSys inputSys;

    private Rigidbody2D rb;

    private void Start()
    {
        // Initialize stats
        dodgerAttributes = new DodgerAttributes(startingHealth, startingHealth, startingScore);

        rb = GetComponent<Rigidbody2D>();

        // Prevent physics from pushing the player
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        float moveDir = 0f;
        Vector2 screenPos;

        // Touch / mouse input
        if (inputSys.IsPressing(out screenPos))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPos.x, screenPos.y, 0f)
            );

            moveDir = touchPos.x < 0 ? -1f : 1f;
        }

        // Prevent leaving screen
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if ((viewportPos.x <= 0f && moveDir < 0f) ||
            (viewportPos.x >= 1f && moveDir > 0f))
        {
            moveDir = 0f;
        }

        // FINAL movement line (this is where it belongs)
        rb.linearVelocity = new Vector2(moveDir * moveSpeed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            int newHealth = dodgerAttributes.GetCurrentHealth() - 1;
            dodgerAttributes.SetCurrentHealth(newHealth);

            Debug.Log("Player hit! Health: " + newHealth);

            if (newHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
