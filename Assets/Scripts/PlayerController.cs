using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();
    void Start()
    {
        _rb2D.velocity = -Vector2.up * _speed;
    }

    public void SetForceDirection(Vector3 Dirction)
    {
        _rb2D.velocity = Vector3.zero;
        _rb2D.velocity = Dirction * _speed;
    }
}
