using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Default keys for Player 1 and Player 2 (keyboard)
    public static KeyCode moveUpKey = KeyCode.W;
    public static KeyCode moveDownKey = KeyCode.S;
    public static KeyCode moveLeftKey = KeyCode.A;
    public static KeyCode moveRightKey = KeyCode.D;
    public static KeyCode attackKey = KeyCode.Space;

    // Default keys for Player 2 (keyboard)
    public static KeyCode moveUpKeyP2 = KeyCode.UpArrow;
    public static KeyCode moveDownKeyP2 = KeyCode.DownArrow;
    public static KeyCode moveLeftKeyP2 = KeyCode.LeftArrow;
    public static KeyCode moveRightKeyP2 = KeyCode.RightArrow;
    public static KeyCode attackKeyP2 = KeyCode.Return;

    // Controller states
    public static bool isPlayer1UsingController = false;
    public static bool isPlayer2UsingController = false;

    // Pause
    public static KeyCode pause = KeyCode.Escape;

    private void Awake()
    {
        LoadKeys();
        SetUpControllers();
    }

    void SetUpControllers()
    {
        // Check if controllers are connected
        string[] joystickNames = Input.GetJoystickNames();
        
        if (joystickNames.Length > 0 && !string.IsNullOrEmpty(joystickNames[0]))
        {
            isPlayer1UsingController = false; // First controller for Player 1
        }
        
        if (joystickNames.Length > 1 && !string.IsNullOrEmpty(joystickNames[1]))
        {
            isPlayer2UsingController = false; // Second controller for Player 2
        }
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
        if (!PlayerPrefs.HasKey("Avancer"))
        {
            PlayerPrefs.SetInt("Avancer", (int)KeyCode.W);
            PlayerPrefs.SetInt("Reculer", (int)KeyCode.S);
            PlayerPrefs.SetInt("Gauche", (int)KeyCode.A);
            PlayerPrefs.SetInt("Droite", (int)KeyCode.D);
            PlayerPrefs.SetInt("AttackKey", (int)KeyCode.Space);

            PlayerPrefs.SetInt("AvancerP2", (int)KeyCode.UpArrow);
            PlayerPrefs.SetInt("ReculerP2", (int)KeyCode.DownArrow);
            PlayerPrefs.SetInt("GaucheP2", (int)KeyCode.LeftArrow);
            PlayerPrefs.SetInt("DroiteP2", (int)KeyCode.RightArrow);
            PlayerPrefs.SetInt("AttackKeyP2", (int)KeyCode.Return);
            
            PlayerPrefs.Save();
        }

        moveUpKey = (KeyCode)PlayerPrefs.GetInt("Avancer", (int)KeyCode.W);
        moveDownKey = (KeyCode)PlayerPrefs.GetInt("Reculer", (int)KeyCode.S);
        moveLeftKey = (KeyCode)PlayerPrefs.GetInt("Gauche", (int)KeyCode.A);
        moveRightKey = (KeyCode)PlayerPrefs.GetInt("Droite", (int)KeyCode.D);
        attackKey = (KeyCode)PlayerPrefs.GetInt("AttackKey", (int)KeyCode.Space);

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

    public static void SetPlayerInputType(int player, bool isUsingController)
    {
        if (player == 1)
        {
            isPlayer1UsingController = isUsingController;
        }
        else if (player == 2)
        {
            isPlayer2UsingController = isUsingController;
        }
    }
}
