using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private Transform _prowTr;
    [SerializeField] private PlayerProjectile _projectilePrefab;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _yBounds = 5.5f;
    [SerializeField] private float _xBounds = 10f;
    [SerializeField] protected int _hitPoints;
    [SerializeField] protected ParticleSystem _explosionPrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _projectileDamage;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _explosionSound;

    private AudioSource _audioSource;
    private float _shootTrigger;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        _healthBar.value = _healthBar.maxValue = _hitPoints;
        _audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isGameOver)
        {
            Rotate();
            MoveUnit();
        }
    }
    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
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
        //vector from player to mouse
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

    public virtual void Hit(int amount)
    {
        _hitPoints -= amount;
        _healthBar.value = _hitPoints;
        if (_hitPoints <= 0)
        {
            DestroyUnit();
        }
    }

    protected virtual void DestroyUnit()
    {
        ParticleSystem explosion = Instantiate(_explosionPrefab, _rb.position, Quaternion.identity);
        Destroy(explosion.gameObject, 1f);
        GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.35f, 0.35f, 1f);
        _audioSource.PlayOneShot(_explosionSound);
        GameManager.Instance.GameOver();
    }
}
