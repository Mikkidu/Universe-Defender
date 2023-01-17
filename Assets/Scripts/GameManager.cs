using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private GameObject _pauseMenu;

    private int _score;
    public int enemyTypes       { get; private set; } 
    public bool isGameOver      { get; private set; }
    public bool isGamePaused    { get; private set; }

    void Start()
    {
        isGamePaused = false;
        _score = 0;
        _gameOverText.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            Pause();
        }
    }

    public void Pause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        _pauseMenu.SetActive(isGamePaused);
        Cursor.visible = isGamePaused;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        _gameOverText.SetActive(true);
    }

    public void AddScore(int value)
    {
        
        _score += value;
        _enemySpawner.IncreaseSpawnRate();
        if (_score > 40) enemyTypes = 3;
        else if (_score > 10) enemyTypes = 2;
        _scoreText.SetText($"Score: {_score}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        DataManager.Instance.SaveCash();
        Cursor.visible = true;
        SceneManager.LoadScene("StartScreen");
        Time.timeScale = 1;
    }
}
