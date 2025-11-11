using System;
using UnityEngine;
using UnityEngine.UIElements;


public class GameUIController : MonoBehaviour
{
    public AudioManagerController AudioManager;
    public SpawnController spawnController;
    private Label gameOverText_;
    private Label score_;
    private Label health_;
    private VisualElement[] lives_;
    public TerrainController TerrainController;
    private Button startButton_;
    private Button restartButton_;
    public int maxLives = 3;
    public int currentLives;
    public int maxHealth = 100;
    private int currentHealth;
    private int currentScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        score_ = root.Q<Label>("Score");
        health_ = root.Q<Label>("Health");
        lives_ = new VisualElement[3];
        lives_[0] = root.Q<VisualElement>("Live-1");
        lives_[1] = root.Q<VisualElement>("Live-2");
        lives_[2] = root.Q<VisualElement>("Live-3");
        startButton_ = root.Q<Button>("StartButton");
        restartButton_ = root.Q<Button>("RestartButton");
        startButton_.clicked += OnStartButtonClicked;
        restartButton_.clicked += OnRestartButtonClicked;
        gameOverText_ = root.Q<Label>("GameOver");
        gameOverText_.style.display = DisplayStyle.None;
        restartButton_.style.display = DisplayStyle.None;
    }
    public void OnDisable()
    {
        startButton_.clicked -= OnStartButtonClicked;
        restartButton_.clicked -= OnRestartButtonClicked;
    }

    void ResetGame()
    {
        currentLives = maxLives;
        currentHealth = maxHealth;
        currentScore = 0;
        health_.text = currentHealth.ToString() + "%";
        SetScore(currentScore);
        foreach (var life in lives_)
        {
            life.style.display = DisplayStyle.Flex;
        }
    }
    void ResetLevel()
    {
        Debug.Log("RESTART LEVEL");
        spawnController.StartSpawn();
    }

    public void OnStartButtonClicked()
    {
        ResetGame();
        gameOverText_.style.display = DisplayStyle.None;
        startButton_.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        TerrainController.StartGame();
        spawnController.StartSpawn();
        AudioManager.PlayMusic();
    }
    public void OnRestartButtonClicked()
    {
        ResetGame();
        gameOverText_.style.display = DisplayStyle.None;
        restartButton_.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        TerrainController.StartGame();
        spawnController.StartSpawn();
        AudioManager.PlayMusic();
    }
    public void SetScore(int score)
    {
        score_.text = score.ToString("D5");
    }

    public void TakeHit(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        health_.text = currentHealth.ToString() + "%";
        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        currentLives--;
        currentHealth = maxHealth;
        health_.text = currentHealth.ToString() + "%";
        if (currentLives >= 0 && currentLives < lives_.Length)
        {
            lives_[currentLives].style.display = DisplayStyle.None;
            spawnController.ClearObjects();
            spawnController.StopSpawn();
            ResetLevel();
        }
        if (currentLives <= 0)
        {
            GameOver();
        }

    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        SetScore(currentScore);
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
        AudioManager.StopMusic();
        spawnController.StopSpawn();
        spawnController.ClearObjects();
        gameOverText_.style.display = DisplayStyle.Flex;
        restartButton_.style.display = DisplayStyle.Flex;
    }
}
