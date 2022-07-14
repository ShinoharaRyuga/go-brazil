using UnityEngine;

/// <summary>
/// マップを上に移動させる
/// 上に移動させることで落下中を表示する。
/// </summary>
public class MapMove : MonoBehaviour
{
    [SerializeField, Min(0.01f),Tooltip("移動速度")] float _moveValue = 0.1f;
    [SerializeField] GameManager _gameManager;

    void Update()
    {
        if (_gameManager.IsGameing)
        {
            transform.Translate(0, _moveValue, 0, Space.World);
        }
    }
}
