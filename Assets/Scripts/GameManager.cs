using UnityEngine;
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

    
    [SerializeField] private EnemySpawner _enemySpawner;



    private DataManager _dManager;
    public int score            { get; private set; }
    public int enemyTypes       { get; private set; }
    public bool isGameOver      { get; private set; }
    public bool isGamePaused    { get; private set; }

    void Start()
    {
        _dManager = DataManager.Instance;
        isGamePaused = false;
        score = 0;
 
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Pause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        UpdateHiScore();

    }

    public void AddScore(int value)
    {
        score += value;
        _enemySpawner.IncreaseSpawnRate();
        if (score > 40) enemyTypes = 3;
        else if (score > 10) enemyTypes = 2;
    }

    private void UpdateHiScore()
    {
        if (score > _dManager.cache.hiScore)
        {
            _dManager.SetHiScore(score);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        _dManager.SaveCash();
        Cursor.visible = true;
        SceneManager.LoadScene("StartScreen");
        Time.timeScale = 1;
    }
}
