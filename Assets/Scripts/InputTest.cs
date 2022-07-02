using UnityEngine;
using TMPro;

/// <summary>スマホの入力を試してみる </summary>
public class InputTest : MonoBehaviour
{
    [SerializeField, Tooltip("タッチされている場所を示すオブジェクト")] GameObject _touchObjectPrefab = default;
    [SerializeField, Tooltip("タップされた回数を表示　デバッグも表示する ")] TextMeshProUGUI _tapCountText = default;
    [SerializeField, Tooltip("フリック入力テストする為オブジェクト")] GameObject _flickTestObject = default;
    [SerializeField, Tooltip("デバッグ情報を表示")] TextMeshProUGUI _debugText = default;
    [SerializeField, Tooltip("ボタンが押された時のテキスト")] TextMeshProUGUI _buttonDebugText = default;
    [SerializeField, Tooltip("フリック入力を受け付ける最小の移動距離")] float _minInputFlickDirection = 20f;
    [SerializeField, Tooltip("フリック入力を受け付ける最大の移動距離")] float _maxInputFlickDirection = 50f;
    [SerializeField, Tooltip("フリックが反応するまでの時間")] float _activateFlickTime = 1f;

    /// <summary>Instantiateしたクローン</summary>
    GameObject _touchObject = default;
    /// <summary>タップ回数 </summary>
    int _tapCount = 0;
    /// <summary>_flickTestObjectのrotate.zの値 </summary>
    int _flickRotateValue = 0;
    /// <summary>前回タッチされたXの位置 </summary>
    float _lasttouchpositionX = 0f;
    /// <summary>フリックでのX移動距離 </summary>
    float _flickDirectionX = 0f;
    /// <summary>フリックでのY移動距離 </summary>
    float _flickDirectionY = 0f;
    /// <summary>画面に指が触れてからの時間　フリックが反応する時間</summary>
    float _flickTime = 0f;
    /// <summary>フリックが反応する時間の計測を開始 </summary>
    bool _isflickTimer = false;
    Vector3 _startPoint = Vector3.zero;
    Vector3 _endPoint = Vector3.zero;
    void Start()
    {
        _touchObject = Instantiate(_touchObjectPrefab, Vector2.zero, Quaternion.identity);
    }

    void Update()
    {
        //foreach (var touch in Input.touches)
        //{
        //    var touchposition = new Vector3(touch.position.x, touch.position.y, 10);
        //    touchposition = Camera.main.ScreenToWorldPoint(touchposition);

        //    if (touch.phase == TouchPhase.Moved)
        //    {
        //        _touchObject.transform.position = touchposition;

        //        if (touch.position.x < _lasttouchpositionX) //左に移動
        //        {
        //            _debugText.text = "左に移動";
        //        }
        //        else if (touch.position.x > _lasttouchpositionX) //右に移動
        //        {
        //            _debugText.text = "右に移動";
        //        }

        //        _tapCountText.text = $"{touch.position.x} {_lasttouchpositionX}";   //x位置を表示
        //        _lasttouchpositionX = touch.position.x;
        //    }
        //}

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
                _buttonDebugText.text = $"移動距離 X{_flickDirectionX} Y{_flickDirectionY}";
               
                //フリック入力が出来ているかどうかの判定
                if (_minInputFlickDirection <= _flickDirectionX && _flickDirectionX <= _maxInputFlickDirection ||
                    _minInputFlickDirection <= _flickDirectionY && _flickDirectionY <= _maxInputFlickDirection &&
                    _flickTime <= _activateFlickTime)
                {
                    _flickRotateValue += 90;
                    _flickTestObject.transform.Rotate(0, 0, _flickRotateValue);

                    if (360 <= _flickRotateValue)
                    {
                        _flickRotateValue = 0;
                    }

                    _debugText.text = "フリック成功";
                }
                else
                {
                    _debugText.text = "フリック失敗";
                }

                _startPoint = Vector3.zero;
                _endPoint = Vector3.zero;
                _flickTime = 0f;
                _isflickTimer = false;
            }
        }

        if (_isflickTimer)
        {
            _flickTime += Time.deltaTime;
            _tapCountText.text = _flickTime.ToString();
        }
    }

    /// <summary>ボタンの機能を試す為の関数 </summary>
    public void DebugButton()
    {
        _buttonDebugText.text = "ボタンが押された";
    }

    /// <summary>ボタンの機能を試す為の関数 </summary>
    public void ResetButton()
    {
        _buttonDebugText.text = "リセット";
    }

    /// <summary>フリックの距離を求める </summary>
    void GetFlickDistance()
    {
        _flickDirectionX = Mathf.Abs(_endPoint.x - _startPoint.x);
        _flickDirectionY = Mathf.Abs(_endPoint.y - _startPoint.y);
    }
}


///入力を受け取る
//foreach (var touch in Input.touches)
//{
//    var touchPhase = touch.phase;

//    switch (touchPhase)
//    {
//        case TouchPhase.Began:
//            _debugText.text = "画面に指が触れた";
//            break;
//        case TouchPhase.Moved:
//            _debugText.text = "画面上で指が動いた";
//            break;
//        case TouchPhase.Ended:
//            _debugText.text = "画面から指が離れた";
//            break;
//        case TouchPhase.Stationary:
//            _debugText.text = "指が画面に触れているが動いてはいない";
//            break;
//        case TouchPhase.Canceled:
//            _debugText.text = "システムがタッチの追跡をキャンセル";
//            break;
//        default:
//            _debugText.text = "触れていない";
//            break;
//    }
//}

//触れている指の数
//if (Input.touchCount == 1)
//{
//    _debugText.text = "押されている";
//    _tapCount++;
//    _tapCountText.text = _tapCount.ToString();
//}


