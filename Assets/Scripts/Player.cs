using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private Transform _prowTr;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _yBounds = 5.5f;
    [SerializeField] private float _xBounds = 10f;

    private float _shootTrigger;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    private void FixedUpdate()
    {
        Rotate();
        MoveUnit();
    }
    private void Update()
    {
        ShootPhase();
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

    protected override void ShootPhase()
    {
        if (Input.GetMouseButton(0) && _shootTrigger < Time.realtimeSinceStartup)
        {
            Projectile bullet = Instantiate(_projectilePrefab, _prowTr.position, transform.rotation);
            Destroy(bullet.gameObject, 2f);
            _shootTrigger = Time.realtimeSinceStartup + _shootDelay;
        }
    }

    void CheckBounds()
    {
        if (_rb.position.x > _xBounds)
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        else if (_rb.position.x < -_xBounds)
            _rb.velocity = new Vector2(0, _rb.velocity.y);

        if (_rb.position.y > _yBounds)
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        else if (_rb.position.y < -_yBounds)
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }
}
