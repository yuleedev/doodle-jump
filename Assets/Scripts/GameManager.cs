using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Camera cam;
    public GameObject mainMenuUI;
    public GameObject gameOverUI;
    public float deathMargin = 1f;

    private bool isGameOver = false;
    private bool hasStarted = false;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (mainMenuUI != null) mainMenuUI.SetActive(true);

        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!hasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space)) StartGame();
            return;
        }

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space)) Restart();
            return;
        }

        float bottomOfScreen = cam.transform.position.y - cam.orthographicSize;

        if (player.position.y < bottomOfScreen - deathMargin)
        {
            GameOver();
        }
    }

    void StartGame()
    {
        hasStarted = true;
        if (mainMenuUI != null) mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void GameOver()
    {
        isGameOver = true;
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}