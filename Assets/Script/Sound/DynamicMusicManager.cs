using UnityEngine;

public class DynamicMusicManager : MonoBehaviour
{
    public AudioSource audioSourceA;  // Première source audio
    public AudioSource audioSourceB;  // Deuxième source audio

    public AudioClip firstMusic;
    public AudioClip secondMusic;
    public float crossfadeDuration = 2f;  // Durée du fondu croisé

    private bool isPlayingSourceA = true;  // Indique quelle source est active

    void Start()
    {
        // Assurez-vous que les deux sources audio sont prêtes
        audioSourceA.volume = 0f;
        audioSourceB.volume = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayMusic(firstMusic);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            PlayMusic(secondMusic);
        }
    }
    public void PlayMusic(AudioClip newClip)
    {
        // Détermine quelle source est actuellement en cours de lecture
        AudioSource activeSource = isPlayingSourceA ? audioSourceA : audioSourceB;
        AudioSource nextSource = isPlayingSourceA ? audioSourceB : audioSourceA;

        // Configure la nouvelle piste pour la source inactive
        nextSource.clip = newClip;
        nextSource.Play();

        // Démarre le fondu croisé
        StartCoroutine(Crossfade(activeSource, nextSource));

        // Bascule la source active
        isPlayingSourceA = !isPlayingSourceA;
    }

    private System.Collections.IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        float time = 0f;

        // Effectue le fondu croisé
        while (time < crossfadeDuration)
        {
            time += Time.deltaTime;
            from.volume = Mathf.Lerp(1f, 0f, time / crossfadeDuration);
            to.volume = Mathf.Lerp(0f, 1f, time / crossfadeDuration);
            yield return null;
        }

        // Assurez-vous que la source précédente est bien arrêtée
        from.Stop();
        from.volume = 0f;
    }
}
