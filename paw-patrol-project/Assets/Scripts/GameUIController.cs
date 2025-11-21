using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameUIController : MonoBehaviour
{
    public TerrainController TerrainController;
    public AudioManagerController AudioManager;
    public SpawnController spawnController;
    public GameManagerController GameManager;
    public BossLevelController BossLevel;
    private Label gameOverText_;
    private Label levelTwo_;
    private Label levelThree_;
    private Label levelFour_;
    private Label level_;
    private Label score_;
    private Label health_;
    private VisualElement[] lives_;
    private Button startButton_;
    private Button restartButton_;
    public bool autoStart = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        if (autoStart)
        {
            // Consulted the internet on how to pause a frame so UI can load to prevent crashes.
            if(PlayerInfoController.level == 2)
            {
                StartCoroutine(StartLevelTwo());
            }
            else if(PlayerInfoController.level == 3)
            {
                StartCoroutine(StartLevelThree());
            }
            else if(PlayerInfoController.level == 4)
            {
                StartCoroutine(StartLevelFour());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        lives_ = new VisualElement[3];

        
        level_ = root.Q<Label>("Level");
        score_ = root.Q<Label>("Score");
        health_ = root.Q<Label>("Health");

        lives_[0] = root.Q<VisualElement>("Live-1");
        lives_[1] = root.Q<VisualElement>("Live-2");
        lives_[2] = root.Q<VisualElement>("Live-3");

        startButton_ = root.Q<Button>("StartButton");
        restartButton_ = root.Q<Button>("RestartButton");
        gameOverText_ = root.Q<Label>("GameOver");
        levelTwo_ = root.Q<Label>("LevelTwo");
        levelThree_ = root.Q<Label>("LevelThree");
        levelFour_ = root.Q<Label>("LevelFour");

        startButton_.clicked += OnStartButtonClicked;
        restartButton_.clicked += OnRestartButtonClicked;
        levelTwo_.style.display = DisplayStyle.None;
        levelThree_.style.display = DisplayStyle.None;
        levelFour_.style.display = DisplayStyle.None;
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
        GameManager.ResetGameData();
        health_.text = GameManager.currentHealth + "%";
        SetScore(GameManager.currentScore);
        foreach (var life in lives_)
        {
            life.style.display = DisplayStyle.Flex;
        }
    }

    void ResetUIOnly()
    {
        GameManager.currentScore  = PlayerInfoController.Score;
        GameManager.currentHealth = PlayerInfoController.Health;
        GameManager.currentLives  = PlayerInfoController.Lives;
        SetScore(GameManager.currentScore);
        health_.text = GameManager.currentHealth + "%";

        for (int i = 0; i < lives_.Length; i++)
        {
            lives_[i].style.display = i < GameManager.currentLives ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
    void ResetLevel()
    {
        Debug.Log("RESTART LEVEL");
        if(PlayerInfoController.level == 3)
        {
            GameManager.ResetBossLevelLogic(BossLevel);
        }
        else
        {
            GameManager.ResetLevelLogic(spawnController);
        }
    }
    public void OnStartButtonClicked()
    {
        ResetGame();
        level_.text = "Level 1";
        levelTwo_.style.display = DisplayStyle.None;
        levelThree_.style.display = DisplayStyle.None;
        gameOverText_.style.display = DisplayStyle.None;
        startButton_.style.display = DisplayStyle.None;
        restartButton_.style.display = DisplayStyle.None;
        GameManager.StartGame(TerrainController, spawnController, AudioManager);
    }
    public void OnNextLevel()
    {
        levelTwo_.style.display = DisplayStyle.None;
        levelThree_.style.display = DisplayStyle.None;
        levelFour_.style.display = DisplayStyle.None;
    }
    public void OnRestartButtonClicked()
    {
        GameManager.RestartGame();
    }

    IEnumerator StartLevelTwo()
    {
        startButton_.style.display = DisplayStyle.None;
        yield return null;
        level_.text = "Level 2";
        levelTwo_.style.display = DisplayStyle.Flex;
        gameOverText_.style.display = DisplayStyle.None;
        GameManager.StartGame(TerrainController, spawnController, AudioManager);
        ResetUIOnly();
        yield return new WaitForSeconds(1f);
        OnNextLevel();
    }

    IEnumerator StartLevelThree()
    {
        startButton_.style.display = DisplayStyle.None;
        yield return null;
        level_.text = "Level 3";
        levelThree_.style.display = DisplayStyle.Flex;
        gameOverText_.style.display = DisplayStyle.None;
        GameManager.StartBossLevel(TerrainController, BossLevel, AudioManager);
        ResetUIOnly();
        yield return new WaitForSeconds(1f);
        OnNextLevel();
    }

    IEnumerator StartLevelFour()
    {
        startButton_.style.display = DisplayStyle.None;
        yield return null;
        level_.text = "Level 4";
        levelFour_.style.display = DisplayStyle.Flex;
        gameOverText_.style.display = DisplayStyle.None;
        GameManager.StartGame(TerrainController, spawnController, AudioManager);
        ResetUIOnly();
        yield return new WaitForSeconds(1f);
        OnNextLevel();
    }

    public void SetScore(int score)
    {
        score_.text = score.ToString("D5");
    }

    public void TakeHit(int damage)
    {
        bool died = GameManager.TakeHit(damage);
        health_.text = GameManager.currentHealth.ToString() + "%";
        if (died)
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        bool gameOver = GameManager.LoseLife();
        health_.text = GameManager.currentHealth.ToString() + "%";
        if (GameManager.currentLives >= 0 && GameManager.currentLives < lives_.Length)
        {
            lives_[GameManager.currentLives].style.display = DisplayStyle.None;
            spawnController.ClearObjects();
            spawnController.StopSpawn();
            ResetLevel();
        }
        if (gameOver)
        {
            GameOver();
        }
    }
    public void AddScore(int amount)
    {
        GameManager.AddScore(amount);
        SetScore(GameManager.currentScore);
    }

    private void GameOver()
    {
        if(PlayerInfoController.level == 3)
        {
            GameManager.TriggerBossGameOver(BossLevel, AudioManager);
        }
        else
        {
            GameManager.TriggerGameOver(spawnController, AudioManager);
        }
        AudioManager.Play(AudioManager.gameOver);
        Debug.Log("GAME OVER");
        gameOverText_.style.display = DisplayStyle.Flex;
        restartButton_.style.display = DisplayStyle.Flex;
    }
    public void ResetHearts()
    {
        foreach (var life in lives_)
        {
            life.style.display = DisplayStyle.Flex;
        }
    }
}
