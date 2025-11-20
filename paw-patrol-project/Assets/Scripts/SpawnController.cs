using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] dogs;
    public GameObject[] leftCar;
    public GameObject[] rightCar;
    public GameObject[] leftKid;
    public GameObject[] rightKid;
    public GameObject specialTreat;
    public float dogSpawnInterval = 4f;
    public float carSpawnInterval = 10f;
    public float kidSpawnInterval = 8f;
    public float corgiSpawnZ = 45f;
    public float germanSpawnZ = -45f;
    public float carLeftSpawnZ = 45f;
    public float carRightSpawnZ = -45f;
    public float kidLeftSpawnZ = 45f;
    public float kidRightSpawnZ = -45f;
    public float leftSidewalkX = -15f;
    public float leftStreetX = -4f;
    public float middleStreetX = 0f;
    public float rightStreetX = 3f;
    public float rightSidewalkX = 15f;
    public float leftBikeLane = -10f;
    public float rightBikeLane = 10f;
    public AudioManagerController AudioManager;
    public GameUIController GameUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()   
    {

    }

    //Randomly spawns dogs
    void SpawnDog()
    {
        int lane = Random.Range(0, 3);
        float spawnX = 0f;

        switch (lane)
        {
            case 0:
                spawnX = leftSidewalkX;
                break;
            case 1:
                spawnX = middleStreetX;
                break;
            case 2:
                spawnX = rightSidewalkX;
                break;
        }
        GameObject spawnDog = dogs[Random.Range(0, dogs.Length)];
        Quaternion rotation = Quaternion.identity;
        float spawnZ = 0f;
        if (spawnDog.name.Contains("germanshepherd"))
        {
            spawnZ = germanSpawnZ;
        }
        if (spawnDog.name.Contains("corgi"))
        {
            spawnZ = corgiSpawnZ;
            rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        Vector3 spawnPos = new Vector3(spawnX, 0f, spawnZ);
        GameObject dog = Instantiate(spawnDog, spawnPos, rotation);
        TargetController targetController = dog.GetComponent<TargetController>();
        if (targetController != null)
        {
            targetController.AudioManager = AudioManager;
            targetController.GameUI = GameUI;
        }
    }
    void SpawnCar()
    {
        GameObject randomLeftCar = leftCar[Random.Range(0, leftCar.Length)];
        Vector3 leftCarPos = new Vector3(leftStreetX, 0f, carLeftSpawnZ);
        Quaternion leftCarRotation = Quaternion.Euler(0f, 180f, 0f);
        GameObject left = Instantiate(randomLeftCar, leftCarPos, leftCarRotation);
        TargetController leftTargetController = left.GetComponent<TargetController>();
        if (leftTargetController != null)
        {
            leftTargetController.AudioManager = AudioManager;
            leftTargetController.GameUI = GameUI;
        }
        GameObject randomRightCar = rightCar[Random.Range(0, rightCar.Length)];
        Vector3 rightCarPos = new Vector3(rightStreetX, 0f, carRightSpawnZ);
        Quaternion rightCarRotation = Quaternion.identity;
        GameObject right = Instantiate(randomRightCar, rightCarPos, rightCarRotation);
        TargetController rightTargetController = right.GetComponent<TargetController>();
        if (rightTargetController != null)
        {
            rightTargetController.AudioManager = AudioManager;
            rightTargetController.GameUI = GameUI;
        }
    }
    void SpawnKid()
    {
        GameObject randomLeftKid = leftKid[Random.Range(0, leftKid.Length)];
        Vector3 leftKidPos = new Vector3(leftBikeLane, 0f, kidLeftSpawnZ);
        Quaternion leftKidRotation = Quaternion.Euler(0f, 180f, 0f);
        GameObject left = Instantiate(randomLeftKid, leftKidPos, leftKidRotation);
        TargetController leftTargetController = left.GetComponent<TargetController>();
        if (leftTargetController != null)
        {
            leftTargetController.AudioManager = AudioManager;
            leftTargetController.GameUI = GameUI;
        }


        GameObject randomRightKid = rightKid[Random.Range(0, rightKid.Length)];
        Vector3 rightKidPos = new Vector3(rightBikeLane, 0f, kidRightSpawnZ);
        Quaternion rightKidRotation = Quaternion.identity;
        GameObject right = Instantiate(randomRightKid, rightKidPos, rightKidRotation);
        TargetController rightTargetController = right.GetComponent<TargetController>();
        if (rightTargetController != null)
        {
            rightTargetController.AudioManager = AudioManager;
            rightTargetController.GameUI = GameUI;
        }
    }
    public void StartSpawn()
    {
        if(PlayerInfoController.level == 1)
        {
            InvokeRepeating(nameof(SpawnDog), 2f, dogSpawnInterval);
            InvokeRepeating(nameof(SpawnCar), 3f, carSpawnInterval);
        }
        else if (PlayerInfoController.level == 2 || PlayerInfoController.level == 4)
        {
            InvokeRepeating(nameof(SpawnDog), 2f, dogSpawnInterval);
            InvokeRepeating(nameof(SpawnCar), 3f, carSpawnInterval);
            InvokeRepeating(nameof(SpawnKid), 3f, kidSpawnInterval);
        }
    }
    public void ClearObjects()
    {
        foreach (GameObject dog in GameObject.FindGameObjectsWithTag("Dog"))
        {
            Destroy(dog);

        }
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Car")) 
        {
            Destroy(car); 
        }
        foreach(GameObject kid in GameObject.FindGameObjectsWithTag("Kid"))
        {
            Destroy(kid);
        }
    }
    public void StopSpawn()
    {
        CancelInvoke(nameof(SpawnDog));
        CancelInvoke(nameof(SpawnCar));
        CancelInvoke(nameof(SpawnKid));
    }
}
