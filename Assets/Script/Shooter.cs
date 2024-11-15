using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int PlayerId;
    private KeyCode shootKey;
    public PauseMenuManager pauseMenuManager;

    private bool isUsingController;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (pauseMenuManager == null) {
            pauseMenuManager = GameObject.Find("PauseManager").GetComponent<PauseMenuManager>();
        }

        if (PlayerId == 1)
        {
            shootKey = InputManager.attackKey;
            isUsingController = InputManager.isPlayer1UsingController;
        } else {
            shootKey = InputManager.attackKeyP2;
            isUsingController = InputManager.isPlayer2UsingController;
        }
    }

    void Update()
    {
        if (pauseMenuManager.pauseMenuUI.activeSelf) return;

        if (!isUsingController)
        {
            if (Input.GetKeyDown(shootKey))
            {
                Shoot();
            }
        } else {
            if (PlayerId == 1)
            {
                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    Shoot();
                }
            }
            else if (PlayerId == 2)
            {
                if (Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            projectilePrefab.GetComponent<Projectile>().playerID = PlayerId;
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
