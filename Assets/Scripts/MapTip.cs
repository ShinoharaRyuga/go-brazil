using UnityEngine;

/// <summary>マップの状態を管理するクラス </summary>
public class MapTip : MonoBehaviour
{
    /// <summary>チップの状態 </summary>
    Status _status = Status.Road;
    /// <summary>状態によって色を変更する為のレンダラー </summary>
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    BoxCollider2D _boxCollider => GetComponent<BoxCollider2D>();
    public Status Status 
    {
        get { return _status; }
        set
        {
            _status = value;
            ChangeColor();
        }
    }

    /// <summary>
    /// 状態によって色を変更
    /// 当たり判定のenebledも変更する
    /// </summary>
    void ChangeColor()
    {
        switch (_status)
        {
            case Status.Road:
                _spriteRenderer.color = Color.black;
                _boxCollider.enabled = false;
                break;
            case Status.Wall:
                _spriteRenderer.color = Color.red;
                _boxCollider.enabled = true;
                break;
            case Status.Test:
                _spriteRenderer.color = Color.blue;
                break;
        }
    }
}

/// <summary>tipの状態 </summary>
public enum Status
{
    Wall,
    Road,
    Test,
}
