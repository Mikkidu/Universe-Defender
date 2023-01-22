using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _hiScoreText;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private GameObject _pauseMenu;

    private GameManager _gManager;
    private DataManager _dManager;
    // Start is called before the first frame update
    void Start()
    {
        _gManager = GameManager.Instance;
        _dManager = DataManager.Instance;
        _gameOverText.SetActive(false);
        UpdateHiScore();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gManager.isGameOver)
        {
            Pause();
        }
    }

    public void UpdateScore(int value)
    {
        _gManager.AddScore(value);
        _scoreText.SetText($"Score: {_gManager.score}");
    }

    private void UpdateHiScore()
    {
        _hiScoreText.SetText($"HiScore: {_dManager.cache.hiScore}");
    }

    public void SetHealthBar(int hp)
    {
        _healthBar.value = _healthBar.maxValue = hp;
    }
    public void UpdateHealthBar(int hp)
    {
        _healthBar.value = hp;
    }


    public void Pause()
    {
        _gManager.Pause();
        _pauseMenu.SetActive(_gManager.isGamePaused);
        Cursor.visible = _gManager.isGamePaused;
    }

    public void GameOver()
    {
        _gManager.GameOver();
        _gameOverText.SetActive(true);
    }

}
