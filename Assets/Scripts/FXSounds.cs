using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSounds : MonoBehaviour
{
    public static FXSounds Instance { get; private set;}
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [SerializeField] private AudioClip[] _enemyExplosion;
    [SerializeField] private AudioClip _enemyShoot;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyExplosion()
    {
        _audioSource.PlayOneShot(_enemyExplosion[Random.Range(0, _enemyExplosion.Length)]);
    }

    public void EnemyShoot()
    {
        _audioSource.PlayOneShot(_enemyShoot);
    }
}
