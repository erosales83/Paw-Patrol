using UnityEngine;

public class TargetController : MonoBehaviour
{

    public float dogSpeed = 6f;
    public float carSpeed = 8f;
    public float kidSpeed = 7f;
    public float villianSpeed = 10f;
    public GameObject rescueEffect;
    public GameObject crashEffect;
    public AudioManagerController AudioManager;
    public GameUIController GameUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Dog"))
        {
            transform.Translate(Vector3.forward * dogSpeed * Time.deltaTime);
        }
        if (gameObject.CompareTag("Car"))
        {
            transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
        }
        if (gameObject.CompareTag("Kid"))
        {
            transform.Translate(Vector3.forward * kidSpeed * Time.deltaTime);
        }
        if (gameObject.CompareTag("Villian"))
        {
            transform.Translate(Vector3.forward * villianSpeed * Time.deltaTime);
        }
    }

    //Collision: If player collides with Dog Game over. If treat collides with dog then particles.
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name.Contains("corgi"))
        {
            if (collision.gameObject.CompareTag("WallBack"))
            {
                if (AudioManager != null)
                {
                    AudioManager.Play(AudioManager.Failed);
                }
                GameUI.TakeHit(25);
                Destroy(gameObject);
            }
        }

        else if (gameObject.name.Contains("germanshepherd"))
        {
            if (collision.gameObject.CompareTag("WallFront"))
            {
                if (AudioManager != null)
                {
                    AudioManager.Play(AudioManager.Failed);
                }
                GameUI.TakeHit(25);
                Destroy(gameObject);
            }
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Car") || gameObject.CompareTag("Kid"))
            {
                if (rescueEffect != null)
                {
                    Instantiate(crashEffect, transform.position, Quaternion.identity);
                    if (AudioManager != null)
                    {
                        AudioManager.Play(AudioManager.Crashed);
                    }
                }
            }
            else
            {
                if (AudioManager != null)
                {
                    AudioManager.Play(AudioManager.Failed);
                }
            }
            GameUI.TakeHit(25);
            Destroy(gameObject);
        }

        if(gameObject.CompareTag("Dog"))
        {
            if (collision.gameObject.CompareTag("Treat"))
            {
                if (AudioManager != null)
                {
                    AudioManager.Play(AudioManager.Explosion);
                }
                if (rescueEffect != null)
                {
                    Instantiate(rescueEffect, transform.position, Quaternion.identity);
                }
                GameUI.AddScore(100);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                
            }
        }
    }
}
