using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 3f;
    [SerializeField] private float volume = 0.4f;

    private void Start()
    {
        // Start the coroutine to play audio
        StartCoroutine(PlayRandomAudio());
    }

    private System.Collections.IEnumerator PlayRandomAudio()
    {
        // Infinite loop to continuously play audio
        while (true)
        {
            // Pick a random audio source
            int randomIndex = Random.Range(0, audioSources.Length);
            AudioSource randomAudio = audioSources[randomIndex];

            //Adjust volume
            randomAudio.volume = volume;
            // Play the audio
            randomAudio.Play();

            // Calculate a random delay before playing the next audio
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }
}
