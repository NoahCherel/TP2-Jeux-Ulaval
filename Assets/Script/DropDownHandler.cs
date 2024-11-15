    using UnityEngine;
    using TMPro; // Assure-toi d'inclure TextMesh Pro

    public class TMPDropdownHandler : MonoBehaviour
    {
        public TMP_Dropdown player1Dropdown; // Référence au Dropdown pour le joueur 1
        public TMP_Dropdown player2Dropdown; // Référence au Dropdown pour le joueur 2

        private void Start()
        {
            // Ajouter des écouteurs pour détecter les changements de sélection
            player1Dropdown.onValueChanged.AddListener(OnPlayer1InputTypeChanged);
            player2Dropdown.onValueChanged.AddListener(OnPlayer2InputTypeChanged);

            // Initialiser les Dropdowns avec la valeur par défaut (Clavier)
            player1Dropdown.value = 0; // "Clavier" par défaut
            player2Dropdown.value = 0; // "Clavier" par défaut
        }

        // Méthode appelée lorsque la sélection du Dropdown du joueur 1 change
        private void OnPlayer1InputTypeChanged(int selectedIndex)
        {
            bool isUsingController = (selectedIndex == 1); // Si index est 1, utiliser la manette
            InputManager.SetPlayerInputType(1, isUsingController); // Mets à jour l'input du joueur 1
        }

        // Méthode appelée lorsque la sélection du Dropdown du joueur 2 change
        private void OnPlayer2InputTypeChanged(int selectedIndex)
        {
            bool isUsingController = (selectedIndex == 1); // Si index est 1, utiliser la manette
            InputManager.SetPlayerInputType(2, isUsingController); // Mets à jour l'input du joueur 2
        }
    }
