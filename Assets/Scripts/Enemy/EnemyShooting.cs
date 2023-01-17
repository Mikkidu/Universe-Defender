using UnityEngine;

public class EnemyShooting : EnemySimple
{
    [SerializeField] private PlayerProjectile _projectilePrefab;
    [SerializeField] private Transform _prowTr;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _projectileDamage;

    private float _shootTrigger;

    protected override void Start()
    {
        base.Start();
        _shootTrigger = Time.realtimeSinceStartup + _reloadTime * 2f;    
    }

    void Update()
    {
        if (!_gManager.isGameOver && !_gManager.isGamePaused)
        {
            ShootPhase();
        }
    }

    private void ShootPhase()
    {
        if (Time.realtimeSinceStartup > _shootTrigger)
        {
            PlayerProjectile bullet = Instantiate(_projectilePrefab, _prowTr.position, transform.rotation);
            bullet.Initialize(_projectileDamage);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * _projectileSpeed;
            bullet.gameObject.layer = 6;
            FXSounds.Instance.EnemyShoot();
            _shootTrigger = Time.realtimeSinceStartup + _reloadTime;
        }
    }
}
