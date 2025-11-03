using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    public AudioSource SoundFX;
    public AudioSource Music;
    public AudioClip BackgroundSound;
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Music.clip = BackgroundSound;
        Music.Play();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Play(AudioClip clip)
    {
        SoundFX.PlayOneShot(clip);
    }
}
