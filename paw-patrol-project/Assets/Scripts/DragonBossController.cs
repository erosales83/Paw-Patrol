using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonBossController : MonoBehaviour
{
    public AudioManagerController AudioManager;
    public GameUIController GameUI;
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
        float targetX = movingRight ? startPos.x + moveRange : startPos.x - moveRange;

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
        if (!collision.gameObject.CompareTag("Treat"))
        {
            return;
        }
        Destroy(collision.gameObject);
        if (AudioManager != null)
        {
            AudioManager.Play(AudioManager.dragonSound);
        }
        Debug.Log("Treat hit dragon!");
        TakeHit();
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
        PlayerInfoController.level = 4;
        Debug.Log("Loading Level 4....");
        SceneManager.LoadScene("AssignmentFinalProjectLevel4");
    }
}
