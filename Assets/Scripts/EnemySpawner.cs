using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private EnemySimple[] _enemies;
    [SerializeField] private float _spawnDelay = 2f; 
    [SerializeField] private Transform _playerTr;
    [SerializeField] private float _xBound = 8.5f;
    [SerializeField] private float _yBound = 6.5f;

    private float _spawnTrigger;
    private GameManager _gManager;
    private float _difficultRate = 1;

    void Start()
    {
        _gManager = GameManager.Instance;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            if (_gManager.isGamePaused) yield return null;
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        int choseEnemy = Random.Range(0, _gManager.enemyTypes);
        EnemySimple enemie = Instantiate(_enemies[choseEnemy], RandomSpawnPosition(), Quaternion.identity);
        enemie.Initialize(_playerTr, _gameUI, _difficultRate);
    }

    private Vector2 RandomSpawnPosition()
    {
        int choseEdge = Random.Range(0, 4);
        switch (choseEdge)
        {
            case 0:
                return new Vector2(Random.Range(-_xBound, _xBound), _yBound);
            case 1:
                return new Vector2(Random.Range(-_xBound, _xBound), -_yBound);
            case 2:
                return new Vector2(_xBound , Random.Range(-_yBound, _yBound));
            default:
                return new Vector2(-_xBound, Random.Range(-_yBound, _yBound));
        }
    }

    public void IncreaseSpawnRate()
    {
        _spawnDelay *= 0.99f;
        _difficultRate *= 1.05f;

    }
}
