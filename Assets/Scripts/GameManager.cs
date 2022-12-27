using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _gameOverText;
    public static GameManager Instance { get; private set; }
    private int _score;
    public int enemyTypes { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool isGameOver { get; private set; }



    void Start()
    {
        Time.timeScale = 1;
        _score = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _gameOverText.SetActive(false);
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
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
