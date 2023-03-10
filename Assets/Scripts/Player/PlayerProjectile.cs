using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerProjectile : MonoBehaviour
{
    protected int _damageAmount;
    [SerializeField] protected ParticleSystem _explosionPrefab;
    
    

    public virtual void Initialize(int damage)
    {
        _damageAmount = damage;

    }
    private void Start()
    {

        Destroy(gameObject, 2f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out Unit target))
        {
            target.Hit(_damageAmount);
            DetonateExplosion(target.gameObject);
        }
    }

    protected virtual void DetonateExplosion(GameObject target)
    {
        target.GetComponent<Rigidbody2D>().AddForce(
                GetComponent<Rigidbody2D>().velocity.normalized * _damageAmount,
                ForceMode2D.Force);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
