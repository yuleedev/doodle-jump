using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int numberOfPlatforms;
    public float levelWidth = 2.25f;
    public float minY = 1.2f;
    public float maxY = 1.8f;
        
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
