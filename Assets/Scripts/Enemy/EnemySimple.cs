using UnityEngine;

public class EnemySimple : Unit
{
    
    [SerializeField] private Transform _playerTr;
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isGameOver)
        {
            Rotate();
            MoveUnit();
        }
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
}
