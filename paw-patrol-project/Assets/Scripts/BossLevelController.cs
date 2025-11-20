using UnityEngine;

public class BossLevelController : MonoBehaviour
{
    public GameObject[] villians;
    public GameObject[] cages;
    public GameObject dragon;
    public float villianSpawnInterval = 2f;
    public float villianFrontSpawnZ = 45f;
    public float villianBackSpawnZ = -45f;
    public float leftSidewalkX = -14f;
    public float leftStreetX = -5f;
    public float middleStreetX = 0f;
    public float rightStreetX = 5f;
    public float rightSidewalkX = 14f;
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

    void SpawnVillian()
    {
        int lane = Random.Range(0, 4);
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
                spawnX = rightStreetX;
                break;
            case 3:
                spawnX = rightSidewalkX;
                break;
        }

        int frontOrBack = Random.Range(0, 2);
        float spawnZ = 0f;
        Quaternion rotation = Quaternion.identity;
        switch(frontOrBack)
        {
            case 0:
                rotation = Quaternion.Euler(0f, 180f, 0f);
                spawnZ = villianFrontSpawnZ;
                break;
            case 1:
                spawnZ = villianBackSpawnZ;
                break;
        }
        GameObject spawnVillian = villians[Random.Range(0, villians.Length)];
        Vector3 spawnPos = new Vector3(spawnX, 0f, spawnZ);
        GameObject villian = Instantiate(spawnVillian, spawnPos, rotation);
        TargetController targetController = villian.GetComponent<TargetController>();
        if (targetController != null)
        {
            targetController.AudioManager = AudioManager;
            targetController.GameUI = GameUI;
        }
    }

    public void StartSpawn()
    {
        InvokeRepeating(nameof(SpawnVillian), 1f, villianSpawnInterval);
    }
    public void ClearObjects()
    {
        foreach(GameObject villian in GameObject.FindGameObjectsWithTag("Villian"))
        {
            Destroy(villian);
        }
    }
    public void StopSpawn()
    {
        CancelInvoke(nameof(SpawnVillian));
    }
}
