using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    public static GameManager Instance { get; private set; }
    private int _score;

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
    }

 
    void Update()
    {
        
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
    }

    public void AddScore(int value)
    {
        _score += value;
        _scoreText.SetText($"Score: {_score}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
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
