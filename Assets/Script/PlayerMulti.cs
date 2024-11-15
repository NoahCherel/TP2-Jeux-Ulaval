using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
 
[RequireComponent(typeof(CharacterController))]
public class FPSController : NetworkBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
 
 
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
 
 
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    float horizontal = 0f;
    float vertical = 0f;
 
    public bool canMove = true;
 
    
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    void Update()
    {
        if (!IsLocalPlayer) return; // Make sure only the local player can control the movement

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        horizontal = Input.GetKey(InputManager.moveLeftKey) ? -1 : (Input.GetKey(InputManager.moveRightKey) ? 1 : 0);
        vertical = Input.GetKey(InputManager.moveUpKey) ? 1 : (Input.GetKey(InputManager.moveDownKey) ? -1 : 0);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * horizontal : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * vertical : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedY) + (right * curSpeedX);

        
        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
    }
}