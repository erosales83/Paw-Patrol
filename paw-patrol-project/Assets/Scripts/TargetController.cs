using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetController : MonoBehaviour
{

    public float speed = 4f;
    public GameObject rescueEffect;
    public AudioSource collisionSound;


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

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collisionSound != null)
            {
                collisionSound.Play();
                Destroy(gameObject, collisionSound.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
            Time.timeScale = 0.0f;
        }
        if (collision.gameObject.CompareTag("Treat"))
        {
            if (collisionSound != null)
            {
                collisionSound.Play();
                Destroy(gameObject, collisionSound.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
            if (rescueEffect != null)
            {
                Instantiate(rescueEffect, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            
        }

    }
}
