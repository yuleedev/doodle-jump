using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 20;
    public float levelWidth = 2.25f;
    public float minY = 1.2f;
    public float maxY = 1.8f;

    [Range(0f, 1f)]
    public float springChance = 0.2f;
    public float springXRange = 0.3f;

    public Camera cam;
    public float recycleMargin = 2f;

    private List<GameObject> platforms = new List<GameObject>();
    private float highestY = 0f;

    void Start()
    {
        if (cam == null) cam = Camera.main;

        Vector3 spawnPosition = new Vector3();

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);

            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            SetupSpring(newPlatform);
            platforms.Add(newPlatform);
        }
        highestY = spawnPosition.y;
    }

    void Update()
    {
        float bottomOfScreen = cam.transform.position.y - cam.orthographicSize;

        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i].transform.position.y < bottomOfScreen - recycleMargin)
            {
                Recycle(platforms[i]);
            }
        }
    }

    void Recycle(GameObject platform)
    {
        highestY += Random.Range(minY, maxY);
        Vector3 newPosition = new Vector3();
        newPosition.y = highestY;
        newPosition.x = Random.Range(-levelWidth, levelWidth);
        platform.transform.position = newPosition;
        SetupSpring(platform);
    }

    void SetupSpring(GameObject platformObject)
    {
        Platform platform = platformObject.GetComponent<Platform>();
        if (platform == null || platform.spring == null) return;

        bool hasSpring = Random.value < springChance;
        platform.spring.SetActive(hasSpring);

        if (hasSpring)
        {
            Vector3 springPos = platform.spring.transform.localPosition;
            springPos.x = Random.Range(-springXRange, springXRange);
            platform.spring.transform.localPosition = springPos;
        }
    }
}