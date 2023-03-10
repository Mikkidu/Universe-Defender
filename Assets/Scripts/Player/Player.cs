using UnityEngine;
using UnityEngine.UI;

public class Player : Unit 
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private Transform _prowTr;
    [SerializeField] private PlayerProjectile _projectilePrefab;

    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private GameUI _gameUI;

    private AudioSource _audioSource;
    private float _shootTrigger;
    private Camera cam;
    private float _yBounds = 5.5f;
    private float _xBounds = 10f;
    private float _reloadTime = 0.5f;
    private float _projectileSpeed = 10f;
    private int _projectileDamage = 10;

    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
        _gameUI.SetHealthBar( _hitPoints);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_gManager.isGameOver && !_gManager.isGamePaused)
        {
            ShootPhase();
        }    
    }

    protected override void MoveUnit()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _rb.AddForce(new Vector2(horizontalInput, verticalInput) * _speed, ForceMode2D.Force);
        CheckBounds();
    }

    protected override void Rotate()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        _crosshair.position = mousePosition;
        mousePosition -= _rb.position;
        _rb.rotation = Vector2.SignedAngle(Vector2.up, mousePosition);
        
    }

    private void ShootPhase() 
    {
        if (Input.GetMouseButton(0) && _shootTrigger < Time.realtimeSinceStartup)
        {
            PlayerProjectile bullet = Instantiate(_projectilePrefab, _prowTr.position, transform.rotation);
            bullet.Initialize(_projectileDamage);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * _projectileSpeed;
            Destroy(bullet.gameObject, 2f);
            _audioSource.PlayOneShot(_shootSound);
            _shootTrigger = Time.realtimeSinceStartup + _reloadTime;
        }
    }

    void CheckBounds()
    {
        if (_rb.position.x > _xBounds && _rb.velocity.x > 0)
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        else if (_rb.position.x < -_xBounds && _rb.velocity.x < 0)
            _rb.velocity = new Vector2(0, _rb.velocity.y);

        if (_rb.position.y > _yBounds && _rb.velocity.y > 0)
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        else if (_rb.position.y < -_yBounds && _rb.velocity.y < 0)
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }

    public override void Hit(int amount) 
    {
        base.Hit(amount);
        _gameUI.UpdateHealthBar(_hitPoints);
    }

    protected override void DestroyUnit() 
    {
        base.DestroyUnit();
        _audioSource.PlayOneShot(_explosionSound);
        GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.35f, 0.35f, 1f);
        _gameUI.GameOver();
    }

}
