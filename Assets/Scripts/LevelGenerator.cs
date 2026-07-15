using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int numberOfPlatforms;
    public float levelWidth = 2.25f;
    public float minY = 1.2f;
    public float maxY = 1.8f;

    [Range(0f, 1f)]
    public float springChance = 0.2f;
    public float springXRange = 0.3f;

    void Start()
    {
        Vector3 spawnPosition = new Vector3();

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);

            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            Platform platform = newPlatform.GetComponent<Platform>();
            if (platform != null && platform.spring != null)
            {
                if (Random.value < springChance)
                {
                    platform.spring.SetActive(true);
                    Vector3 springPos = platform.spring.transform.localPosition;
                    springPos.x = Random.Range(-springXRange, springXRange);
                    platform.spring.transform.localPosition = springPos;
                }
            }
        }
    }
}