using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class RecordDataExample : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameData gameData;

    public float speed = 5f;
    public int score = 0;



    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction moveAction;

    float recordTimer = 0; //Add later

    public float recordEvery = 1f; //Add later

    private void Awake()
    {
        gameData = new GameData();
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        // Move player
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0);
        player.transform.Translate(move * speed * Time.deltaTime);

        recordTimer += Time.deltaTime; //Add later

        // Only record data if player is moving
        if (moveInput != Vector2.zero && recordTimer > recordEvery) //Add later the  && recordTimer > recordEvery
        {
            PlayerData data = new PlayerData();

            recordTimer = 0; //Add later

            data.time = Time.time;
            data.posX = player.transform.position.x;
            data.posY = (int)player.transform.position.z;
            score++;
            data.score = score;

            

            gameData.entries.Add(data);
        }
    }

    
}