using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public int maxHealth = 100;
    public int currentHealth;
    public int currentScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeHit(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        return currentHealth <= 0;
    }

    public bool LoseLife()
    {
        currentLives--;
        currentHealth = maxHealth;
        return currentLives <= 0;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public void TriggerGameOver(SpawnController spawner, AudioManagerController audio)
    {
        currentHealth = 0;
        currentLives = 0;
        Time.timeScale = 0f;
        audio.StopMusic();
        spawner.StopSpawn();
        spawner.ClearObjects();
    }
    public void TriggerBossGameOver(BossLevelController spawner, AudioManagerController audio)
    {
        currentHealth = 0;
        currentLives = 0;
        Time.timeScale = 0f;
        audio.StopMusic();
        spawner.StopSpawn();
        spawner.ClearObjects();
    }

    public void ResetLevelLogic(SpawnController spawnController)
    {
        spawnController.StartSpawn();
    }

    public void ResetBossLevelLogic(BossLevelController bossLevel)
    {
        bossLevel.StartSpawn();
    }

    public void StartGame(TerrainController terrain, SpawnController spawner, AudioManagerController audio)
    {
        Time.timeScale =1f;
        terrain.StartGame();
        spawner.StartSpawn();
        audio.PlayMusic();
    }
    public void StartBossLevel(TerrainController terrain, BossLevelController boss, AudioManagerController audio)
    {
        Time.timeScale =1f;
        terrain.StartGame();
        audio.PlayBossMusic();
        boss.StartSpawn();
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        PlayerInfoController.level = 1;
        SceneManager.LoadScene("AssignmentFinalProject");
    }
    public void ResetGameData()
    {
        currentLives = maxLives;
        currentHealth = maxHealth;
        currentScore = 0;
    }
}
