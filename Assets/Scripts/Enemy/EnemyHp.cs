using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] protected int _hitPoints;
    [SerializeField] protected ParticleSystem _explosionPrefab;
    [SerializeField] private int _damageAmount;
    [SerializeField] private int _scorePoints;

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
        ParticleSystem explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, 1f);
        GameManager.Instance.AddScore(_scorePoints);
        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Hit(_damageAmount);
            player.GetComponent<Rigidbody2D>().AddForce(
                GetComponent<Rigidbody2D>().velocity.normalized * _damageAmount / 5f,
                ForceMode2D.Impulse);
            ParticleSystem explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Hit(_hitPoints);
        }
    }
}
