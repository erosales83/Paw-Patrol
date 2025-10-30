using UnityEngine;

public class TreatController : MonoBehaviour

{

    public float speed = 10f;
    public AudioSource shootSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shootSound != null)
            {
                shootSound.Play();
            }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
