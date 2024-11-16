using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

[RequireComponent(typeof(CharacterController))]
public class FPSController : NetworkBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    private float horizontal = 0f;
    private float vertical = 0f;

    public bool canMove = true;

    private CharacterController characterController;

    // NetworkVariables to sync position and rotation
    private NetworkVariable<Vector3> syncedPosition = new NetworkVariable<Vector3>(
        writePerm: NetworkVariableWritePermission.Owner); // Owner updates, others read.
    private NetworkVariable<Quaternion> syncedRotation = new NetworkVariable<Quaternion>(
        writePerm: NetworkVariableWritePermission.Owner); // Owner updates, others read.

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!IsOwner) // Non-owners update position and rotation from the server
        {
            transform.position = syncedPosition.Value;
            transform.rotation = Quaternion.Lerp(transform.rotation, syncedRotation.Value, Time.deltaTime * 10); // Smooth rotation
            return;
        }

        HandleMovement();
        HandleRotation();

        // Update the synced position and rotation
        syncedPosition.Value = transform.position;
        syncedRotation.Value = transform.rotation;
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        horizontal = Input.GetKey(InputManager.moveLeftKey) ? -1 : (Input.GetKey(InputManager.moveRightKey) ? 1 : 0);
        vertical = Input.GetKey(InputManager.moveUpKey) ? 1 : (Input.GetKey(InputManager.moveDownKey) ? -1 : 0);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * horizontal : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * vertical : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedY) + (right * curSpeedX);

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner)
        {
            // Disable camera and audio listener for non-owners
            playerCamera.enabled = false;

            if (playerCamera.TryGetComponent(out AudioListener audioListener))
            {
                audioListener.enabled = false;
            }
        }
        else
        {
            // Lock the cursor for the owning player
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
