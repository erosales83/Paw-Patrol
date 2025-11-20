using Unity.VisualScripting;
using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    public AudioSource SoundFX;
    public AudioSource Music;
    public AudioClip BackgroundSound;
    public AudioClip BossSound;
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Explosion;
    public AudioClip Failed;
    public AudioClip Crashed;
    public AudioClip gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(AudioClip clip)
    {
        SoundFX.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        Music.clip = BackgroundSound;
        Music.Play();
    }

    public void StopMusic()
    {
        Music.Stop();
    }

    public void PlayBossMusic()
    {
        Music.clip = BossSound;
        Music.Play();
    }
}
