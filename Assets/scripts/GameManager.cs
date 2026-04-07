using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] brickPref;
    public float spawnRate;

    bool gameStarted = false;

    public GameObject tapText;
    public TextMeshProUGUI scoreText;

    int score = 0;
    Vector2 screenPos;

    [SerializeField] private Player player;

    private float timer = 0f;
    private float gameTime = 0f;

    void Update()
    {
        
        if (GetComponent<InputSys>().IsPressing(out screenPos) && !gameStarted)
        {
            StartSpawning();
            gameStarted = true;
            tapText.SetActive(false);
        }

        
        if (gameStarted)
        {
            gameTime += Time.deltaTime;
            timer += Time.deltaTime;
        }
    }

    void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnBrick), 0.5f, spawnRate);
    }

    void SpawnBrick()
    {
        Camera cam = Camera.main;

        float randomX = Random.Range(0f, 1f);
        Vector3 viewportPos = new Vector3(randomX, 1f, 0f);
        Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);

        worldPos.y += 1f;
        worldPos.z = 0f;

        int index = Random.Range(0, brickPref.Length);

        Instantiate(brickPref[index], worldPos, Quaternion.identity);

        score++;

        player.dodgerAttributes.SetCurrentScore(score);

        UpdateText(score);
    }

    public void AddScore(int amount)
    {
        int newScore = player.dodgerAttributes.GetCurrentScore() + amount;
        player.dodgerAttributes.SetCurrentScore(newScore);
        Debug.Log("Score updated: " + newScore);
    }

    void UpdateText(int score)
    {
        scoreText.text = score.ToString();
    }

}