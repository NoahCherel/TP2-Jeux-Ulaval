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

    // Référence au InputManager pour savoir si le joueur utilise le clavier ou la manette
    private bool isUsingController;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Récupérer si le joueur utilise un clavier ou une manette
        if (PlayerId == 1)
        {
            shootKey = InputManager.attackKey;
            isUsingController = InputManager.isPlayer1UsingController; // Vérifie si Player1 utilise la manette
        }
        else
        {
            shootKey = InputManager.attackKeyP2;
            isUsingController = InputManager.isPlayer2UsingController; // Vérifie si Player2 utilise la manette
        }
    }

    void Update()
    {
        // Si le menu est en pause, on ne traite pas les entrées
        if (pauseMenuManager.pauseMenuUI.activeSelf) return;

        // Si le joueur utilise le clavier
        if (!isUsingController)
        {
            if (Input.GetKeyDown(shootKey))
            {
                Shoot();
            }
        }
        // Si le joueur utilise la manette
        else
        {
            if (Input.GetButtonDown("Fire" + PlayerId)) // "Fire1" ou "Fire2" selon le joueur
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Instancie le projectile au niveau de firePoint
        if (projectilePrefab != null && firePoint != null)
        {
            projectilePrefab.GetComponent<Projectile>().playerID = PlayerId;
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
