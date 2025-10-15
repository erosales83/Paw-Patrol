using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetController : MonoBehaviour
{

    public float speed = 4f;
    public GameObject rescueEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.0f;
        }
        if (collision.gameObject.CompareTag("Treat"))
        {
            if (rescueEffect != null)
            {
                Instantiate(rescueEffect, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
