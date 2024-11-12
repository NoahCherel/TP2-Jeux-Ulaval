using UnityEngine;

public class SplitScreenController : MonoBehaviour
{
    public Camera playerCamera1;  // Camera for Player 1
    public Camera playerCamera2;  // Camera for Player 2
    public GameObject player1;    // Player 1 GameObject
    public GameObject player2;    // Player 2 GameObject
    public float cameraHeight = 20f;  // Camera height above the players
    public float moveSpeed = 6f;  // Player movement speed

    private Vector3 player1Position;
    private Vector3 player2Position;

    private CharacterController player1Controller;
    private CharacterController player2Controller;

    void Start()
    {
        SetUpSplitScreen();

        // Get the CharacterControllers for both players
        player1Controller = player1.GetComponent<CharacterController>();
        player2Controller = player2.GetComponent<CharacterController>();
    }

    void Update()
    {
        player1Position = player1.transform.position;
        player2Position = player2.transform.position;

        UpdateCameraPositions();

        HandlePlayerMovement(player1, player1Controller, 1);
        HandlePlayerMovement(player2, player2Controller, 2);
    }

    void SetUpSplitScreen()
    {
        playerCamera1.rect = new Rect(0f, 0f, 0.5f, 1f);  // Left half
        playerCamera2.rect = new Rect(0.5f, 0f, 0.5f, 1f);  // Right half
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
            // Contrôles du joueur 1
            horizontal = Input.GetKey(InputManager.moveLeftKey) ? -1 : (Input.GetKey(InputManager.moveRightKey) ? 1 : 0);
            vertical = Input.GetKey(InputManager.moveUpKey) ? 1 : (Input.GetKey(InputManager.moveDownKey) ? -1 : 0);
        }
        else if (playerNumber == 2)
        {
            // Contrôles du joueur 2
            horizontal = Input.GetKey(InputManager.moveLeftKeyP2) ? -1 : (Input.GetKey(InputManager.moveRightKeyP2) ? 1 : 0);
            vertical = Input.GetKey(InputManager.moveUpKeyP2) ? 1 : (Input.GetKey(InputManager.moveDownKeyP2) ? -1 : 0);
        }

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.deltaTime;
        characterController.Move(movement);
    }
}
