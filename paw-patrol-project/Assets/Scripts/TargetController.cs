using UnityEngine;

public class TargetController : MonoBehaviour
{

    public float dogSpeed = 6f;
    public float carSpeed = 8f;
    public float kidSpeed = 7f;
    public float villianSpeed = 12f;
    public float PowerUpSpeed = 12f;
    public GameObject rescueEffect;
    public GameObject crashEffect;
    public AudioManagerController AudioManager;
    public GameUIController GameUI;
    public SpawnController spawner;
    public BossLevelController bossLevel;
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
        if (gameObject.CompareTag("PowerUp"))
        {
            transform.Translate(Vector3.forward * PowerUpSpeed * Time.deltaTime);
        }
    }

    //Collision: If player collides with Dog Game over. If treat collides with dog then particles.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            return;
        }
        if (gameObject.name.Contains("corgi") && collision.gameObject.CompareTag("WallBack"))
        {
            if (AudioManager != null)
            {
                AudioManager.Play(AudioManager.Failed);
            }
            GameUI.TakeHit(25);
            Destroy(gameObject);
        }
        if (gameObject.name.Contains("germanshepherd") && collision.gameObject.CompareTag("WallFront"))
        {
            if (AudioManager != null)
            {
                AudioManager.Play(AudioManager.Failed);
            }
            GameUI.TakeHit(25);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("PowerUp"))
            {
                GameUI.AddScore(100);
                if(spawner != null)
                {
                    spawner.ClearBadObjects();
                }
                if (bossLevel != null)
                {
                    bossLevel.ClearBadObjects();
                }
                if (AudioManager != null)
                {
                    AudioManager.Play(AudioManager.specialTreatSound);
                }
            }
            else
            {
                if (gameObject.CompareTag("Car") || gameObject.CompareTag("Kid") || gameObject.CompareTag("Villian"))
                {
                    if (rescueEffect != null)
                    {   
                        Instantiate(crashEffect, transform.position, Quaternion.identity);
                        if (AudioManager != null)
                        {
                            AudioManager.Play(AudioManager.Crashed);
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
                }
            }
            Destroy(gameObject);
            return;
    }
        if(gameObject.CompareTag("Dog") && collision.gameObject.CompareTag("Treat"))
        {
            if (AudioManager != null)
            {
                GameUI.AddScore(100);
                AudioManager.Play(AudioManager.Explosion);
            }
            if (rescueEffect != null)
            {
                Instantiate(rescueEffect, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(gameObject.CompareTag("PowerUp") && collision.gameObject.CompareTag("Treat")) 
        {
            if (AudioManager != null)
            {           
                GameUI.AddScore(100);
                if(spawner != null)
                {
                    spawner.ClearBadObjects();
                }
                if (bossLevel != null)
                {
                    bossLevel.ClearBadObjects();
                }     
                AudioManager.Play(AudioManager.specialTreatSound);
            }
            if (rescueEffect != null)
            {                    
                Instantiate(rescueEffect, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
