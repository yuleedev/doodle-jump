using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Camera cam;
    public GameObject mainMenuUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public float deathMargin = 1f;
    public float scoreMultiplier = 10f;

    private bool isGameOver = false;
    private bool hasStarted = false;
    private float startY;
    private int score = 0;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (mainMenuUI != null) mainMenuUI.SetActive(true);
        if (scoreText != null) scoreText.text = "0";

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

        UpdateScore();

        float bottomOfScreen = cam.transform.position.y - cam.orthographicSize;

        if (player.position.y < bottomOfScreen - deathMargin)
        {
            GameOver();
        }
    }

    void UpdateScore()
    {
        int heightScore = Mathf.RoundToInt((player.position.y - startY) * scoreMultiplier);
        if (heightScore > score)
        {
            score = heightScore;
            if (scoreText != null) scoreText.text = score.ToString();
        }
    }

    void StartGame()
    {
        hasStarted = true;
        startY = player.position.y;
        score = 0;
        if (mainMenuUI != null) mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void GameOver()
    {
        isGameOver = true;

        int best = PlayerPrefs.GetInt("HighScore", 0);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score + "\nBest: " + best;

        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}