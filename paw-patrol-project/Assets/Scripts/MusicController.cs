using UnityEngine;
using UnityEngine.LightTransport;

public class MusicController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource[] randomSounds;
    public float minDelay = 4f;
    public float maxDelay = 10f;

    private float nextPlayTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        nextRandomSound();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Stop();
            }
            foreach (var sound in randomSounds)
            {
                if (sound.isPlaying)
                {
                    sound.Stop();
                }
                return;
            }
        }
        if (randomSounds.Length > 0 && Time.time >= nextPlayTime)
        {
            int randomIndex = Random.Range(0, randomSounds.Length);
            randomSounds[randomIndex].Play();
            nextRandomSound();
        }
    }
    void nextRandomSound()
    {
        nextPlayTime = Time.time + Random.Range(minDelay, maxDelay);
    }
}
