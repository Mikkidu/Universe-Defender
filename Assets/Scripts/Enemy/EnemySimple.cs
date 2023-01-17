using UnityEngine;


public class EnemySimple : Unit 
{
    
    [SerializeField] protected Transform _playerTr;
    [SerializeField] protected int _scorePoints;
    [SerializeField] private int _damageAmount;
    private Vector3 _vel = Vector3.zero;


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
        _rb.MoveRotation(Vector2.SignedAngle(Vector2.up, toPlayer));
    }


    protected override void DestroyUnit()
    {
        base.DestroyUnit();
        FXSounds.Instance.EnemyExplosion();
        _gManager.AddScore(_scorePoints);
        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Hit(_damageAmount);
            //Explosive knockback.
            player.GetComponent<Rigidbody2D>().AddForce(
                GetComponent<Rigidbody2D>().velocity.normalized * _damageAmount / 5f,
                ForceMode2D.Impulse);
            Hit(_hitPoints);
        }
    }
}
