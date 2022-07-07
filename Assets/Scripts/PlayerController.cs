using UnityEngine;

/// <summary>プレイヤーの落下速度を変更する </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("落下速度")] float _speed = 1f;
    Rigidbody2D _rb2D = default;

    public Rigidbody2D Rb2D { get => _rb2D; set => _rb2D = value; }

    /// <summary>落下速度 </summary>
    public float Speed 
    {
        get => _speed;
        set
        {
            if (0 < value)
            {
                _speed = value;
            }
        }
    }

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
