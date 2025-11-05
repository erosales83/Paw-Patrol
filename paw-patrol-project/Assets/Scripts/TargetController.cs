using UnityEngine;

public class TargetController : MonoBehaviour
{

    public float speed = 4f;
    public GameObject rescueEffect;
    public AudioManagerController AudioManager;
    public GameUIController GameUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    //Collision: If player collides with Dog Game over. If treat collides with dog then particles.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Player"))
        {
            if (AudioManager != null)
            {
                AudioManager.Play(AudioManager.Failed);
            }
            GameUI.TakeHit(25);
            Destroy(gameObject);
        }
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
