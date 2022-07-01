using System.Collections;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField, Tooltip("回転速度")] float _rotateSpeed = 0.1f;
    [SerializeField, Tooltip("回転方向")] RotateDirection _rotateDirection = RotateDirection.Normal;
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
            ChangeMoveDirection(true);
        }
        else if (h == -1)
        {
            ChangeMoveDirection(false);
        }

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < _lasttouchpositionX) //左に移動
                {
                    ChangeMoveDirection(false);
                }
                else if (touch.position.x > _lasttouchpositionX) //右に移動
                {
                    ChangeMoveDirection(true);
                }

                _lasttouchpositionX = touch.position.x;
            }
        }
    }

    /// <summary>_rotateDirectionによって回転方向を変える</summary>
    /// <param name="InputDirection">true=右入力されている　false=左入力されている</param>
    public void ChangeMoveDirection(bool InputDirection)
    {
        if (_rotateDirection == RotateDirection.Normal)
        {
            if (InputDirection)
            {
                transform.Rotate(0, 0, _rotateSpeed);
            }
            else
            {
                transform.Rotate(0, 0, -_rotateSpeed);
            }
        }
        else if (_rotateDirection == RotateDirection.Reverse)
        {
            if (InputDirection)
            {
                transform.Rotate(0, 0, -_rotateSpeed);
            }
            else
            {
                transform.Rotate(0, 0, _rotateSpeed);
            }
        }
    } 

    public IEnumerator ResetRotate()
    {
        yield return new WaitForSeconds(2f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("リセット");
    }

    

    enum RotateDirection
    {
        /// <summary>入力した方向に回転する</summary>
        Normal,
        /// <summary>入力した方向とは逆回転する</summary>
        Reverse
    }
}
