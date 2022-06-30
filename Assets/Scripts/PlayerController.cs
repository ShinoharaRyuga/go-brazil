using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    Rigidbody2D _rb2D = default;

    public Rigidbody2D Rb2D { get => _rb2D; set => _rb2D = value; }

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public void SetForceDirection(Vector3 Dirction)
    {
        _rb2D.velocity = Vector3.zero;
        _rb2D.velocity = Dirction * _speed;
    }

    public void AddForce()
    {
        _rb2D.velocity = -Vector2.up * _speed;
    }
}
