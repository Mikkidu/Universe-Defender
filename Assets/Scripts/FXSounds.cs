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

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
