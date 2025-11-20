using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLevelController : MonoBehaviour
{
    public float levelDuration = 40f;
    private float timer = 0f;
    public GameManagerController GameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= levelDuration)
        {
            PlayerInfoController.Score = GameManager.currentScore;
            PlayerInfoController.Health = GameManager.currentHealth;
            PlayerInfoController.Lives  = GameManager.currentLives;
            string currentScene = SceneManager.GetActiveScene().name;
            if(currentScene == "AssignmentFinalProject")
            {
                PlayerInfoController.level = 2;
                Debug.Log("Loading Level 2....");
                SceneManager.LoadScene("AssignmentFinalProjectLevel2");
            }
            else
            {
                PlayerInfoController.level = 3;
                Debug.Log("Loading Level 3....");
                SceneManager.LoadScene("AssignmentFinalProjectLevel3");
            }
        }
    }
}
