using UnityEngine;

/// <summary>
/// マップを上に移動させる
/// 上に移動させることで落下中を表現する
/// </summary>
public class MapMove : MonoBehaviour
{
    [SerializeField, Min(0.01f),Tooltip("移動速度")] float _moveValue = 0.1f;
    [SerializeField] GameManager _gameManager;

    public float MoveValue 
    {
        get { return _moveValue; }
        set
        {
            if (0 < value)
            {
                _moveValue = value;
            }
        }
    }

    void Update()
    {
        if (_gameManager.IsGameing)
        {
            transform.Translate(0, _moveValue, 0, Space.World);
        }
    }
}
