using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Unit : MonoBehaviour
{
    protected Rigidbody2D _rb;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _hitPoints;
    [SerializeField] protected ParticleSystem _explosionPrefab;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance.isGameOver)
        {
            Rotate();
            MoveUnit();
        }
    }

    protected abstract void MoveUnit(); 

    protected abstract void Rotate();

    /// <summary>
    /// Decreases unit's health
    /// </summary>
    /// <param name="amount"></param>
    public virtual void Hit(int amount)
    {
        _hitPoints -= amount;
        if (_hitPoints <= 0)
        {
            DestroyUnit();
        }
    }

    /// <summary>
    /// Called at zero health. Destroys a unit and scores points.
    /// </summary>
    protected virtual void DestroyUnit()
    {
        ParticleSystem explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, 1f);
    }
}
