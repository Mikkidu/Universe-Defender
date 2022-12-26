using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySimple[] _enemies;
    [SerializeField] private float _spawnDelay = 2f; 
    [SerializeField] private Transform _playerTr;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _xBound = 8.5f;
    [SerializeField] private float _yBound = 6.5f;

    private float _spawnTrigger;

    void Start()
    {
        _spawnTrigger = Time.realtimeSinceStartup + _spawnDelay;
    }


    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (Time.realtimeSinceStartup > _spawnTrigger)
        {

            int choseEnemy = Random.Range(0, GameManager.Instance.enemyTypes);
            EnemySimple enemie = Instantiate(_enemies[choseEnemy], RandomSpawnPosition(), Quaternion.identity);
            enemie.Initialize(_playerTr);
            _spawnTrigger = Time.realtimeSinceStartup + _spawnDelay;
        }
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
    }
}
