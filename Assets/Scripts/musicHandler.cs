using UnityEngine;

public class musicHandler : MonoBehaviour
{
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private float volume = 0.4f;

    private void Start()
    {
        // Set the volume for both audio sources
        introSource.volume = volume;
        loopSource.volume = volume;

        // Start playing the intro music
        introSource.Play();

        // Start playing the looped music after the intro has finished
        Invoke("PlayLoopedMusic", introSource.clip.length - 0.75f);
    }

    private void PlayLoopedMusic()
    {
        // Set loop to true for the looped audio source
        loopSource.loop = true;
        // Start playing the looped music
        loopSource.Play();
    }
}
