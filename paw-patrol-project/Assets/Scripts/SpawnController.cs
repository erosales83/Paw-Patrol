using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] dogs;
    public float spawnInterval = 6f;
    public float spawnrange = 40f;
    public float spawnZ = 20f;
    public float leftSidewalkX = -15f;
    public float leftStreetX = -5f;
    public float middleStreetX = 0f;
    public float rightStreetX = 5f;
    public float rightSidewalkX = 15f;
    public AudioManagerController AudioManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnDog), 1f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Randomly spawns dogs
    void SpawnDog()
    {
        int lane = Random.Range(0, 5);
        float spawnX = 0f;

        switch (lane)
        {
            case 0:
                spawnX = leftSidewalkX;
                break;
            case 1:
                spawnX = leftStreetX;
                break;
            case 2:
                spawnX = middleStreetX;
                break;
            case 3:
                spawnX = rightStreetX;
                break;
            case 4:
                spawnX = rightSidewalkX;
                break;
        }

        Vector3 spawnPos = new Vector3(spawnX, 0f, spawnZ);
        GameObject spawnDog = dogs[Random.Range(0, dogs.Length)];
        GameObject dog = Instantiate(spawnDog, spawnPos, Quaternion.identity);

        TargetController targetController = dog.GetComponent<TargetController>(); 
        if (targetController != null)
        {
            targetController.AudioManager = AudioManager;
        } 
    }
}
