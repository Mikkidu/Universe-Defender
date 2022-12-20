using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : Unit
{
    
    [SerializeField] private Transform _playerTr;
    [SerializeField] private int _damageAmount;


    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Rotate();
        MoveUnit();
    }



    public virtual void Initialize(Transform player)
    {
        _playerTr = player;
    }

    protected override void MoveUnit()
    {
        _rb.velocity = transform.up * _speed; 
    }

    protected override void Rotate()
    {
        Vector2 toPlayer = (Vector2)_playerTr.position - _rb.position;
        _rb.rotation = Vector2.SignedAngle(Vector2.up, toPlayer);
    }

    protected override void ShootPhase()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Hit(_damageAmount);
            player.GetComponent<Rigidbody2D>().AddForce(_rb.velocity, ForceMode2D.Impulse);
            ParticleSystem explosion = Instantiate(_explosionPrefab, _rb.position, Quaternion.identity);
            Hit(_hitPoints);
        }
    }



}
