using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private EnemyProjectile _projectilePrefab;
    [SerializeField] private Transform _prowTr;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _projectileDamage;

    private float _shootTrigger;

    private void Start()
    {
        _shootTrigger = Time.realtimeSinceStartup + _reloadTime * 2f;    
    }

    void Update()
    {
        ShootPhase();
    }

    private void ShootPhase()
    {
        if (Time.realtimeSinceStartup > _shootTrigger)
        {
            EnemyProjectile bullet = Instantiate(_projectilePrefab, _prowTr.position, transform.rotation);
            bullet.Initialize(_projectileDamage);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * _projectileSpeed;
            Destroy(bullet.gameObject, 2f);
            _shootTrigger = Time.realtimeSinceStartup + _reloadTime;
        }
    }
}
