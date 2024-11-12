using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TopDownController : MonoBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 6f; // Adjusted to control movement speed
    public float gravity = 10f;

    // Références aux touches configurées par InputManager
    private KeyCode moveForwardKey;
    private KeyCode moveBackwardKey;
    private KeyCode moveLeftKey;
    private KeyCode moveRightKey;

    private int playerID; // Identifiant du joueur (1 ou 2)

    Vector3 moveDirection = Vector3.zero;

    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        // Définir la caméra pour la vue du dessus
        playerCamera.transform.position = new Vector3(transform.position.x, 20f, transform.position.z); // Position de la caméra
        playerCamera.transform.rotation = Quaternion.Euler(90f, 0, 0); // Rotation de la caméra pour regarder vers le bas

        // Déterminer quel joueur ce contrôleur gère
        playerID = gameObject.CompareTag("Player2") ? 2 : 1;
        Debug.Log(playerID);

        // Charger les contrôles personnalisés en fonction du joueur
        LoadCustomControls();
    }

    void Update()
    {
        #region Handle Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Contrôles de mouvement pour Player 1 ou Player 2
        float curSpeedX = canMove ? (Input.GetKey(moveForwardKey) ? 1 : (Input.GetKey(moveBackwardKey) ? -1 : 0)) : 0;
        float curSpeedY = canMove ? (Input.GetKey(moveRightKey) ? 1 : (Input.GetKey(moveLeftKey) ? -1 : 0)) : 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        #endregion

        #region Move the Player
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        #endregion
    }

    // Charger les touches personnalisées depuis InputManager
    void LoadCustomControls()
    {
        // Charger les touches du joueur actuel (1 ou 2)
        if (playerID == 1)
        {
            moveForwardKey = InputManager.moveUpKey;
            moveBackwardKey = InputManager.moveDownKey;
            moveLeftKey = InputManager.moveLeftKey;
            moveRightKey = InputManager.moveRightKey;
        }
        else if (playerID == 2)
        {
            moveForwardKey = InputManager.moveUpKeyP2;
            moveBackwardKey = InputManager.moveDownKeyP2;
            moveLeftKey = InputManager.moveLeftKeyP2;
            moveRightKey = InputManager.moveRightKeyP2;
        }
    }
}
