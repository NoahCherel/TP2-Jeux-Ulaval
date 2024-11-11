using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        // Set up the cameras for split-screen
        SetUpSplitScreen();
    }

    void Update()
    {
        // Update player positions
        player1Position = player1.transform.position;
        player2Position = player2.transform.position;

        // Adjust cameras based on positions
        UpdateCameraPositions();

        // Handle player movement (simplified example for both players)
        HandlePlayerMovement(player1);
        HandlePlayerMovement(player2);
    }

    // Set up split-screen camera views
    void SetUpSplitScreen()
    {
        // Player 1 camera view (left side of the screen)
        playerCamera1.rect = new Rect(0f, 0f, 0.5f, 1f);  // Left half

        // Player 2 camera view (right side of the screen)
        playerCamera2.rect = new Rect(0.5f, 0f, 0.5f, 1f);  // Right half
    }

    // Update the camera positions to follow each player
    void UpdateCameraPositions()
    {
        // Player 1 camera follows Player 1
        Vector3 player1CameraPosition = new Vector3(player1Position.x, cameraHeight, player1Position.z);
        playerCamera1.transform.position = player1CameraPosition;
        playerCamera1.transform.LookAt(player1Position);

        // Player 2 camera follows Player 2
        Vector3 player2CameraPosition = new Vector3(player2Position.x, cameraHeight, player2Position.z);
        playerCamera2.transform.position = player2CameraPosition;
        playerCamera2.transform.LookAt(player2Position);
    }

    // Handle player movement (simplified for this example)
    void HandlePlayerMovement(GameObject player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        player.transform.Translate(movement);
    }
}
