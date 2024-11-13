using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Préfab du projectile
    public Transform firePoint; // Point d'origine des tirs
    public int PlayerId; // Identifiant du joueur
    private KeyCode shootKey; // Touche de tir
    public PauseMenuManager pauseMenuManager;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (PlayerId == 1)
        {
            shootKey = InputManager.attackKey;
        }
        else 
        {
            shootKey = InputManager.attackKeyP2;
        }
    }

    void Update()
    {
        if (pauseMenuManager.pauseMenuUI.activeSelf) return;
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instancie le projectile au niveau de firePoint
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
