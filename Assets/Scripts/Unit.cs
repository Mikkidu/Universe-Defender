using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Unit : MonoBehaviour
{
    protected Rigidbody2D _rb;
    [SerializeField] protected float _speed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }



    protected abstract void MoveUnit();

    protected abstract void Rotate();
    
}
