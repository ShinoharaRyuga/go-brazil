using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField, Tooltip("回転速度")] float _rotateSpeed = 0.1f;

    /// <summary>前回タッチされたXの位置 </summary>
    float _lasttouchpositionX = 0f;

    public float RotateSpeed
    {
        get { return _rotateSpeed; }
        set
        {
            if (0 < value)
            {
                _rotateSpeed = value;
            }
        }
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");

        if (h == 1)
        {
            transform.Rotate(0, 0, _rotateSpeed);
        }
        else if (h == -1)
        {
            transform.Rotate(0, 0, -_rotateSpeed);
        }

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < _lasttouchpositionX) //左に移動
                {
                    transform.Rotate(0, 0, _rotateSpeed);
                }
                else if (touch.position.x > _lasttouchpositionX) //右に移動
                {
                    transform.Rotate(0, 0, -_rotateSpeed);
                }

                _lasttouchpositionX = touch.position.x;
            }
        }

    }
}
