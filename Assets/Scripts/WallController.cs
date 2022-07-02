using System.Collections;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField, Tooltip("回転速度")] float _rotateSpeed = 0.1f;
    [SerializeField, Tooltip("回転方向")] RotateDirection _rotateDirection = RotateDirection.Normal;
    [SerializeField, Tooltip("フリック入力を受け付ける最小の移動距離")] float _minInputFlickDirection = 20f;
    [SerializeField, Tooltip("フリック入力を受け付ける最大の移動距離")] float _maxInputFlickDirection = 50f;
    [SerializeField, Tooltip("フリックが反応するまでの時間")] float _activateFlickTime = 1f;
    /// <summary>画面に指が触れた時の場所 </summary>
    Vector3 _startPoint = Vector3.zero;
    /// <summary>画面に指が離れた時の場所 </summary>
    Vector3 _endPoint = Vector3.zero;
    /// <summary>_flickTestObjectのrotate.zの値 </summary>
    int _flickRotateValue = 0;
    /// <summary>フリックでのX移動距離 </summary>
    float _flickDirectionX = 0f;
    /// <summary>フリックでのY移動距離 </summary>
    float _flickDirectionY = 0f;
    /// <summary>前回タッチされたXの位置 </summary>
    float _lasttouchpositionX = 0f;
    /// <summary>画面に指が触れてからの時間　フリックが反応する時間</summary>
    float _flickTime = 0f;
    /// <summary>フリックが反応する時間の計測を開始 </summary>
    bool _isflickTimer= false;

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

        foreach (var touch in Input.touches)    //スワイプ入力
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

        foreach (var touch in Input.touches)    //フリック入力
        {
            if (touch.phase == TouchPhase.Began)
            {
                _startPoint = new Vector3(touch.position.x, touch.position.y, 10);
                _isflickTimer = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _endPoint = new Vector3(touch.position.x, touch.position.y, 10);
                GetFlickDistance();
            
                //フリック入力が出来ているかどうかの判定
                if (_minInputFlickDirection <= _flickDirectionX && _flickDirectionX <= _maxInputFlickDirection      //X軸のフリック
                    || _minInputFlickDirection <= _flickDirectionY && _flickDirectionY <= _maxInputFlickDirection   //Y軸のフリック
                    && _flickTime <= _activateFlickTime)   //フリックが発動するかどうか
                {
                    _flickRotateValue += 90;
                     transform.Rotate(0, 0, _flickRotateValue);

                    if (360 <= _flickRotateValue)
                    {
                        _flickRotateValue = 0;
                    }

                    _flickTime = 0f;
                    _isflickTimer = false;
                }
               
                _startPoint = Vector3.zero;
                _endPoint = Vector3.zero;
            }
        }

        if (_isflickTimer)
        {
            _flickTime += Time.deltaTime;
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

    /// <summary>フリックの距離を求める </summary>
    void GetFlickDistance()
    {
        _flickDirectionX = Mathf.Abs(_endPoint.x - _startPoint.x);
        _flickDirectionY = Mathf.Abs(_endPoint.y - _startPoint.y);
    }


    enum RotateDirection
    {
        /// <summary>入力した方向に回転する</summary>
        Normal,
        /// <summary>入力した方向とは逆回転する</summary>
        Reverse
    }
}
