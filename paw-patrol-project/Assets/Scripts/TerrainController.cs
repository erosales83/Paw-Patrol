using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> spawnedStreet = new List<GameObject>();
    public float speed = 5f;
    Vector3 position;
    private bool gameStarted = false;

    void Start()
    {
        float startingPosition = 0;

        for (int i = 0; i < 10; i++)
        {
            position = new Vector3(0.0f, 0.0f, 0.0f);
            // Create an instance of the prefab.
            GameObject obj = Instantiate(prefab, position, Quaternion.identity);

            // Initialize the move component.
            obj.transform.position = new Vector3(0, 0, startingPosition);
            spawnedStreet.Add(obj);

            startingPosition += 100;
        }
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!gameStarted)
        {
            return;
        }
        foreach (GameObject street in spawnedStreet)
        {
            street.transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (street.transform.position.z < -100)
            {
                float farStreet = -Mathf.Infinity;
                foreach (GameObject s in spawnedStreet)
                {
                    if (s.transform.position.z > farStreet)
                    {
                        farStreet = s.transform.position.z;
                    }
                    street.transform.position = new Vector3(0, 0, farStreet + 100);
                }
            }
        }
    }
    public void StartGame()
    {
        Debug.Log("Game Started!");
        gameStarted = true;
        Time.timeScale = 1f;
    }
}