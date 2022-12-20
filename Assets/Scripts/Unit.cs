using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Unit : MonoBehaviour
{
    protected Rigidbody2D _rb;
    [SerializeField] protected int _hitPoints;
    [SerializeField] protected ParticleSystem _explosion;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }



    protected abstract void MoveUnit();

    protected abstract void Rotate();
    //protected abstract void ShootPhase();
    public virtual void Hit(int amount)
    {
        _hitPoints -= amount;
        if (_hitPoints <= 0)
        {
            DestroyUnit();
        }
    }

    protected virtual void DestroyUnit()
    {
        ParticleSystem explosion = Instantiate(_explosion, _rb.position, Quaternion.identity);
        Destroy(explosion.gameObject, 2f);
        Destroy(gameObject);
    }
}
