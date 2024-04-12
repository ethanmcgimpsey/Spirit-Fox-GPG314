using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject[] spawners;
    public float spawnInterval = 5f;
    public float objectDuration = 10f;
    public float timer = 3f;

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new object
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f; // Reset the timer
        }
    }

    void SpawnObject()
    {
        int selectedIndex = Random.Range(0, spawners.Length);
        
        // Instantiate the object prefab
        GameObject newObject = Instantiate(objectPrefab, spawners[selectedIndex].transform.position, Quaternion.identity);

        // Destroy the object after a certain duration
        Destroy(newObject, objectDuration);
    }
}
