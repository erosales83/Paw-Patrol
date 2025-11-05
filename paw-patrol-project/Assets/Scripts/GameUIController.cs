using UnityEngine;
using UnityEngine.UIElements;


public class GameUIController : MonoBehaviour
{
    public AudioManagerController AudioManager;
    private Label gameOverText_;
    private Label score_;
    private Label health_;
    private VisualElement[] lives_;
    public TerrainController TerrainController;
    private Button startButton_;
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
        startButton_.clicked += OnStartButtonClicked;
        gameOverText_ = root.Q<Label>("GameOver");
        gameOverText_.style.display = DisplayStyle.None;
    }
    public void OnDisable()
    {
        startButton_.clicked -= OnStartButtonClicked;
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

    public void OnStartButtonClicked()
    {
        ResetGame();
        startButton_.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        TerrainController.StartGame();
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
        gameOverText_.style.display = DisplayStyle.Flex;
    }
}
