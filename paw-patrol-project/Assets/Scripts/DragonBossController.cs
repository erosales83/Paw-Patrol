using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonBossController : MonoBehaviour
{
    public AudioManagerController AudioManager;
    public GameManagerController GameManager;
    public GameUIController GameUI;
    public BossLevelController bossLevel;
    public float PowerUpSpeed = 12f;
    public int maxHits = 20;
    private int currentHits;
    public float moveSpeed = 5f;
    public float moveRange = 15f;
    private Vector3 startPos;
    private bool movingRight = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHits = 0;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSideToSide();
    }

    void MoveSideToSide()
    {
        float targetX;

        if (movingRight)
        {
            targetX = startPos.x + moveRange;
        }
        else
        {
            targetX = startPos.x - moveRange;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetX, transform.position.y, transform.position.z),
            moveSpeed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( gameObject.CompareTag("Boss") && collision.gameObject.CompareTag("Treat"))
        {
            Destroy(collision.gameObject);
            if (AudioManager != null)
            {
                AudioManager.Play(AudioManager.dragonSound);
            }
            TakeHit();
            return;
        }
        if(gameObject.CompareTag("PowerUp") && collision.gameObject.CompareTag("Treat"))
        {
            GameUI.AddScore(100);
            bossLevel.ClearBadObjects();
            if (AudioManager != null)
            {
                AudioManager.Play(AudioManager.specialTreatSound);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return;
        } 
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("PowerUp"))
            {
                GameUI.AddScore(100);
                bossLevel.ClearBadObjects();
                if (AudioManager != null)
                {
                    AudioManager.Play(AudioManager.specialTreatSound);
                }
                Destroy(gameObject);
            }
            return;
        }
    }
    void TakeHit()
    {
        currentHits++;
        if (currentHits >= maxHits)
            DefeatBoss();
    }

    void DefeatBoss()
    {
        Debug.Log("Boss Defeated!");

        GameUI.AddScore(1000);
        Destroy(gameObject);
        PlayerInfoController.Score = GameManager.currentScore;
        PlayerInfoController.Health = GameManager.currentHealth;
        PlayerInfoController.Lives  = GameManager.currentLives;
        PlayerInfoController.level = 4;
        Debug.Log("Loading Level 4....");
        SceneManager.LoadScene("AssignmentFinalProjectLevel4");
    }
}
