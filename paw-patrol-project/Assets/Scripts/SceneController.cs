using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject prefab;
    Vector3 position;

    void Start()
    {
        float startingPosition = 0;

        for (int i = 0; i < 10; i++)
        {
            position = new Vector3(0.0f, 0.0f, 0.0f);
            // Create an instance of the prefab.
            GameObject obj = Instantiate(prefab, position, Quaternion.identity);

            // Initialize the move component.
            obj.transform.position = new Vector3(
                0, 0, startingPosition);
            startingPosition = startingPosition + 100;
        }      
    }
}