using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private float spawnDelay = 2, spawnTrigger;
    [SerializeField] private Transform player;
    [SerializeField] private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnTrigger = Time.realtimeSinceStartup + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > spawnTrigger && !_gameManager.isGameOver)
        {
            int choseEnemy = Random.Range(0, enemies.Length);
            GameObject enemie = Instantiate(enemies[choseEnemy], RandomSpawnPosition(), Quaternion.identity);
            //enemie.GetComponent<EnemySimple>(). = player;
            //enemie.GetComponent<HitThePlayer>().gameObject = gameObject;
            spawnTrigger = Time.realtimeSinceStartup + spawnDelay;
        }
    }

    private Vector2 RandomSpawnPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 10;
    }
}
