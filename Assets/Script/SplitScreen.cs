using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplitScreenController : MonoBehaviour
{
    public Camera playerCamera1;  
    public Camera playerCamera2;  
    public GameObject player1;    
    public GameObject player2;    
    public float cameraHeight = 20f;  
    public float moveSpeed = 6f;  
    public PlayerHealth player1Health;
    public PlayerHealth player2Health;

    public PlayerScore player1Score;
    public PlayerScore player2Score;
    public TMP_Text player1HPText;
    public TMP_Text player2HPText;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    private Vector3 player1Position;
    private Vector3 player2Position;

    private CharacterController player1Controller;
    private CharacterController player2Controller;

    public PauseMenuManager pauseMenuManager;

    void Start()
    {
        SetUpSplitScreen();

        player1Controller = player1.GetComponent<CharacterController>();
        player2Controller = player2.GetComponent<CharacterController>();

        // Initialize health displays
        setUpPlayerHealth();
        setUpPlayerScore();
        UpdateHealthUI();
        UpdateScoreUI();
    }

    void Update()
    {
        if (pauseMenuManager.pauseMenuUI.activeSelf) return;

        player1Position = player1.transform.position;
        player2Position = player2.transform.position;

        UpdateCameraPositions();

        HandlePlayerMovement(player1, player1Controller, 1);
        HandlePlayerMovement(player2, player2Controller, 2);

        UpdateHealthUI();  // Met à jour l'UI des PVs
        UpdateScoreUI();
    }

    void SetUpSplitScreen()
    {
        playerCamera1.rect = new Rect(0f, 0f, 0.5f, 1f);  
        playerCamera2.rect = new Rect(0.5f, 0f, 0.5f, 1f);  
    }

    void setUpPlayerHealth()
    {
        // Adjust Player 1 HP Text to be in top-left corner of left screen
        player1HPText.rectTransform.anchorMin = new Vector2(0, 1);
        player1HPText.rectTransform.anchorMax = new Vector2(0, 1);
        player1HPText.rectTransform.anchoredPosition = new Vector2(100, -10); // Slight padding from corner

        // Adjust Player 2 HP Text to be in top-left corner of right screen
        player2HPText.rectTransform.anchorMin = new Vector2(0.5f, 1); // Right screen starts at 0.5 width
        player2HPText.rectTransform.anchorMax = new Vector2(0.5f, 1);
        player2HPText.rectTransform.anchoredPosition = new Vector2(100, -10); // Padding from corner
    }

    void setUpPlayerScore()
    {
        // Adjust Player 1 Score Text to be in top-left corner of left screen
        player1ScoreText.rectTransform.anchorMin = new Vector2(0, 1);
        player1ScoreText.rectTransform.anchorMax = new Vector2(0, 1);
        player1ScoreText.rectTransform.anchoredPosition = new Vector2(400, -10); // Slight padding from corner

        // Adjust Player 2 Score Text to be in top-left corner of right screen
        player2ScoreText.rectTransform.anchorMin = new Vector2(0.5f, 1); // Right screen starts at 0.5 width
        player2ScoreText.rectTransform.anchorMax = new Vector2(0.5f, 1);
        player2ScoreText.rectTransform.anchoredPosition = new Vector2(400, -10); // Padding from corner
    }

    void UpdateCameraPositions()
    {
        Vector3 player1CameraPosition = new Vector3(player1Position.x, cameraHeight, player1Position.z);
        playerCamera1.transform.position = player1CameraPosition;
        playerCamera1.transform.LookAt(player1Position);

        Vector3 player2CameraPosition = new Vector3(player2Position.x, cameraHeight, player2Position.z);
        playerCamera2.transform.position = player2CameraPosition;
        playerCamera2.transform.LookAt(player2Position);
    }

    void HandlePlayerMovement(GameObject player, CharacterController characterController, int playerNumber)
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (playerNumber == 1)
        {
            horizontal = Input.GetKey(InputManager.moveLeftKey) ? -1 : (Input.GetKey(InputManager.moveRightKey) ? 1 : 0);
            vertical = Input.GetKey(InputManager.moveUpKey) ? 1 : (Input.GetKey(InputManager.moveDownKey) ? -1 : 0);
        }
        else if (playerNumber == 2)
        {
            horizontal = Input.GetKey(InputManager.moveLeftKeyP2) ? -1 : (Input.GetKey(InputManager.moveRightKeyP2) ? 1 : 0);
            vertical = Input.GetKey(InputManager.moveUpKeyP2) ? 1 : (Input.GetKey(InputManager.moveDownKeyP2) ? -1 : 0);
        }

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            player.transform.forward = movement;
        }

        characterController.Move(movement);
    }

    void UpdateHealthUI()
    {
        player1HPText.text = "HP: " + player1Health.currentHealth;
        player2HPText.text = "HP: " + player2Health.currentHealth;
    }

    void UpdateScoreUI()
    {
        player1ScoreText.text = "Score: " + player1Score.currentScore;
        player2ScoreText.text = "Score: " + player2Score.currentScore;
    }
}
