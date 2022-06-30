using UnityEngine;
using TMPro;

/// <summary>スマホの入力を試してみる </summary>
public class InputTest : MonoBehaviour
{
    [SerializeField, Tooltip("タッチされている場所を示すオブジェクト")] GameObject _touchObjectPrefab = default;
    [SerializeField, Tooltip("タップされた回数を表示　デバッグも表示する ")] TextMeshProUGUI _tapCountText = default;
    [SerializeField, Tooltip("デバッグ情報を表示")] TextMeshProUGUI _debugText = default;
    [SerializeField, Tooltip("ボタンが押された時のテキスト")] TextMeshProUGUI _buttonDebugText = default;
    /// <summary>Instantiateしたクローン</summary>
    GameObject _touchObject = default;
    /// <summary>タップ回数 </summary>
    int _tapCount = 0;
    /// <summary>前回タッチされたXの位置 </summary>
    float _lasttouchpositionX = 0f;
    void Start()
    {
        _touchObject = Instantiate(_touchObjectPrefab, Vector2.zero, Quaternion.identity);
    }

    void Update()
    {
        foreach (var touch in Input.touches)
        {
            var touchposition = new Vector3(touch.position.x, touch.position.y, 10);
            touchposition = Camera.main.ScreenToWorldPoint(touchposition);

            if (touch.phase == TouchPhase.Moved)
            {
                _touchObject.transform.position = touchposition;

                if (touch.position.x < _lasttouchpositionX) //左に移動
                {
                    _debugText.text = "左に移動";
                }
                else if (touch.position.x > _lasttouchpositionX) //右に移動
                {
                    _debugText.text = "右に移動";
                }

                _tapCountText.text = $"{touch.position.x} {_lasttouchpositionX}";   //x位置を表示
                _lasttouchpositionX = touch.position.x;
            }
        }
    }

    /// <summary>ボタンの機能を試す為の関数 </summary>
    public void DebugButton()
    {
        _buttonDebugText.text = "ボタンが押された";
    }

    public void ResetButton()
    {
        _buttonDebugText.text = "リセット";
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


