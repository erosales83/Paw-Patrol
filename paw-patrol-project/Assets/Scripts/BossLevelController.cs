using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelController : MonoBehaviour
{
    public GameObject[] villians;
    public GameObject dragon;
    public GameObject[] specialTreat;
    public float treatSpawnInterval = 15f;
    public float villianSpawnInterval = 1f;
    public float villianFrontSpawnZ = 45f;
    public float villianBackSpawnZ = -45f;
    public float leftSidewalkX = -14f;
    public float leftStreetX = -5f;
    public float middleStreetX = 0f;
    public float rightStreetX = 5f;
    public float rightSidewalkX = 14f;
    private GameObject dragonInstance;
    public AudioManagerController AudioManager;
    public GameUIController GameUI;
    public GameManagerController gameManager;

    
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
                spawnX = rightStreetX;
                break;
            case 3:
                spawnX = rightSidewalkX;
                break;
            case 4:
                spawnX = middleStreetX;
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
        TargetController target = villian.GetComponent<TargetController>();
        if (target != null)
        {
            target.AudioManager = AudioManager;
            target.GameUI = GameUI;
            target.gameManager = gameManager;
            target.spawner = null;
            target.bossLevel = this;
        }
    }
    void SpawnSpecialtreat()
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
                spawnX = rightStreetX;
                break;
            case 3:
                spawnX = rightSidewalkX;
                break;
            case 4:
                spawnX = middleStreetX;
                break;
        }

        int frontOrBack = Random.Range(0, 2);
        float spawnZ = 0f;
        Quaternion rotation = Quaternion.identity;
        switch(frontOrBack)
        {
            case 0:
                spawnZ = -45f;
                break;
            case 1:
                rotation = Quaternion.Euler(0f, 180f, 0f);
                spawnZ = 45f;
                break;
        }

        GameObject special = specialTreat[Random.Range(0, specialTreat.Length)];
        Vector3 spawnPos = new Vector3(spawnX, 0f, spawnZ);
        GameObject treat = Instantiate(special, spawnPos, rotation);
        TargetController target = treat.GetComponent<TargetController>();
        if (target != null)
        {
            target.AudioManager = AudioManager;
            target.GameUI = GameUI;
            target.gameManager = gameManager;
            target.spawner = null;
            target.bossLevel = this;
        }
    }

    public void StartSpawn()
    {
        SpawnDragon();
        InvokeRepeating(nameof(SpawnVillian), 1f, villianSpawnInterval);
        InvokeRepeating(nameof(SpawnSpecialtreat), 3f, treatSpawnInterval);
    }
    public void ClearObjects()
    {
        foreach(GameObject villian in GameObject.FindGameObjectsWithTag("Villian"))
        {
            Destroy(villian);
        }
        foreach(GameObject treat in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(treat);
        }
    }
    public void ClearBadObjects()
    {
        foreach(GameObject villian in GameObject.FindGameObjectsWithTag("Villian"))
        {
            Destroy(villian);
        }
    }
    public void ClearDogObjects()
    {
        foreach (GameObject dog in GameObject.FindGameObjectsWithTag("Dog"))
        {
            Destroy(dog);

        }
    }
    public void StopSpawn()
    {
        CancelInvoke(nameof(SpawnVillian));
        CancelInvoke(nameof(SpawnSpecialtreat));
    }
    void SpawnDragon()
    {
        if(PlayerInfoController.level != 3)
        {
            return;
        }
        if(dragonInstance != null)
        {
            return;
        }
        Vector3 pos = new Vector3(0,0, 35f);
        dragonInstance = Instantiate(dragon, pos, Quaternion.Euler(0f, 180f, 0f));
        DragonBossController boss = dragonInstance.GetComponent<DragonBossController>();
        if (boss != null)
        {
            boss.AudioManager = AudioManager;
            boss.GameUI = GameUI;
            boss.GameManager = gameManager;
            boss.bossLevel = this;
        }
    }
}
