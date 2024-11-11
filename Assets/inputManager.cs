using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Player 1
    public static KeyCode moveUpKey = KeyCode.W;
    public static KeyCode moveDownKey = KeyCode.S;
    public static KeyCode moveLeftKey = KeyCode.A;
    public static KeyCode moveRightKey = KeyCode.D;
    public static KeyCode attackKey = KeyCode.Space;
    
    // Player 2
    public static KeyCode moveUpKeyP2 = KeyCode.UpArrow;
    public static KeyCode moveDownKeyP2 = KeyCode.DownArrow;
    public static KeyCode moveLeftKeyP2 = KeyCode.LeftArrow;
    public static KeyCode moveRightKeyP2 = KeyCode.RightArrow;
    public static KeyCode attackKeyP2 = KeyCode.Return;

    private void Awake()
    {
        LoadKeys();
    }

    public static void SetKey(string keyName, KeyCode key, int player = 1)
    {
        if (keyName.Contains("P2"))
        {
            player = 2;
        }
        PlayerPrefs.SetInt(keyName, (int)key);
        PlayerPrefs.Save();
        LoadKeys();
    }

    public static void LoadKeys()
    {
        // Si c'est la première exécution, initialise les touches par défaut
        if (!PlayerPrefs.HasKey("Avancer"))
        {
            PlayerPrefs.SetInt("Avancer", (int)KeyCode.W);
            PlayerPrefs.SetInt("Reculer", (int)KeyCode.S);
            PlayerPrefs.SetInt("Gauche", (int)KeyCode.A);
            PlayerPrefs.SetInt("Droite", (int)KeyCode.D);
            PlayerPrefs.SetInt("AttackKey", (int)KeyCode.Space);

            // Touches par défaut pour Player 2
            PlayerPrefs.SetInt("AvancerP2", (int)KeyCode.UpArrow);
            PlayerPrefs.SetInt("ReculerP2", (int)KeyCode.DownArrow);
            PlayerPrefs.SetInt("GaucheP2", (int)KeyCode.LeftArrow);
            PlayerPrefs.SetInt("DroiteP2", (int)KeyCode.RightArrow);
            PlayerPrefs.SetInt("AttackKeyP2", (int)KeyCode.Return);
            
            PlayerPrefs.Save();
        }

        // Charge les touches depuis PlayerPrefs
        moveUpKey = (KeyCode)PlayerPrefs.GetInt("Avancer", (int)KeyCode.W);
        moveDownKey = (KeyCode)PlayerPrefs.GetInt("Reculer", (int)KeyCode.S);
        moveLeftKey = (KeyCode)PlayerPrefs.GetInt("Gauche", (int)KeyCode.A);
        moveRightKey = (KeyCode)PlayerPrefs.GetInt("Droite", (int)KeyCode.D);
        attackKey = (KeyCode)PlayerPrefs.GetInt("AttackKey", (int)KeyCode.Space);

        // Player 2
        moveUpKeyP2 = (KeyCode)PlayerPrefs.GetInt("AvancerP2", (int)KeyCode.UpArrow);
        moveDownKeyP2 = (KeyCode)PlayerPrefs.GetInt("ReculerP2", (int)KeyCode.DownArrow);
        moveLeftKeyP2 = (KeyCode)PlayerPrefs.GetInt("GaucheP2", (int)KeyCode.LeftArrow);
        moveRightKeyP2 = (KeyCode)PlayerPrefs.GetInt("DroiteP2", (int)KeyCode.RightArrow);
        attackKeyP2 = (KeyCode)PlayerPrefs.GetInt("AttackKeyP2", (int)KeyCode.Return);
    }

    public static KeyCode GetKeyCodeForKey(string keyName, int player = 1)
    {
        if (keyName.Contains("P2"))
        {
            player = 2;
        }
        keyName = keyName.Replace("P2", "");

        switch (keyName)
        {
            case "Avancer":
                return player == 1 ? moveUpKey : moveUpKeyP2;
            case "Reculer":
                return player == 1 ? moveDownKey : moveDownKeyP2;
            case "Gauche":
                return player == 1 ? moveLeftKey : moveLeftKeyP2;
            case "Droite":
                return player == 1 ? moveRightKey : moveRightKeyP2;
            case "AttackKey":
                return player == 1 ? attackKey : attackKeyP2;
            default:
                return KeyCode.None;
        }
    }
}
