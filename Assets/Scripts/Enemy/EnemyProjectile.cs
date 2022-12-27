using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : PlayerProjectile // INHERITANCE
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player target))
        {
            target.Hit(_damageAmount);
            DetonateExplosion(target.gameObject);
        }
    }
}
