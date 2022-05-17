using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    // * References
    public static GameManager Instance = null;
    GameObject winPanel;
    GameObject gameOverPanel;
    TextMeshProUGUI timerText;
    Button restartButton;
    // *Variables
    [SerializeField] int gridSize = 3;
    [SerializeField] float startTime = 11f;
    float currentTime = 0f;
    bool win = false;
    bool gameOver = false;
    // * Props
    public int GridSize { get { return gridSize; } private set { } }
    // * Methods
    private void Awake()
    {
        SingletonGameManager();
        FindReferences();
        gameOverPanel.transform.GetChild(0).gameObject.SetActive(false);
        currentTime = startTime;
    }
    private void Update()
    {
        if (gameOver)
            return;
        currentTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.FloorToInt(currentTime).ToString();
        if (currentTime < 1f && !win)
            GameOver();
    }
    void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
        gameOverPanel.transform.GetChild(0).gameObject.SetActive(true);
        Reload();
    }
    public void Win()
    {
        if (win)
            return;
        win = true;
        Time.timeScale = 0f;
        winPanel.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(NextLevel());
    }
    public void Reload()
    {
        StartCoroutine(Retry());
    }
    void FindReferences()
    {
        winPanel = GameObject.FindGameObjectWithTag("WinPanel");
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>();
    }
    void SingletonGameManager()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }
    // * Coroutines
    IEnumerator NextLevel()
    {
        gameOver = false;
        gridSize++;
        startTime += 20f;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(0.1f);
        FindReferences();
        currentTime = startTime;
        win = false;
    }
    IEnumerator Retry()
    {
        win = false;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(0.1f);
        FindReferences();
        currentTime = startTime;
        gameOverPanel.transform.GetChild(0).gameObject.SetActive(false);
        gameOver = false;
    }
}