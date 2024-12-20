using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public GameObject MainMenu; // Référence au GameObject MainMenu
    public GameObject ChooseGameMode; // Référence au GameObject ChooseGameMode

    public float animationDuration = 1f; // Durée de l'animation

    public Vector3 offScreenPosition; // Position de sortie pour MainMenu

    public void OnPlayButtonClicked()
    {
        // 1. Déplacer MainMenu hors écran
        LeanTween.move(MainMenu, offScreenPosition, animationDuration)
                 .setEase(LeanTweenType.easeInOutQuad)
                 .setOnComplete(() =>
                 {
                     // Désactiver le MainMenu
                     MainMenu.SetActive(false);

                     // 2. Activer ChooseGameMode et le ramener à sa position normale
                     ChooseGameMode.SetActive(true);
                     ChooseGameMode.transform.position = new Vector3(956, 548, 2000); // Position initiale hors écran
                     LeanTween.move(ChooseGameMode, new Vector3(956, 548, -1103), animationDuration)
                              .setEase(LeanTweenType.easeInOutQuad);
                 });
    }
}
