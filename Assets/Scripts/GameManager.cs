using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームを管理する
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _player = default;
    [SerializeField, Tooltip("プレイヤーが操作する壁")] WallController _wall = default;
    [SerializeField, Tooltip("ゲーム開始ボタン")] Button _startButton = default;
    [SerializeField, Tooltip("壁の回転速度を変える為のドロップダウン")] TMP_Dropdown _speedDropdown = default;
    [SerializeField, Tooltip("")] TMP_Dropdown _playerFallDropdown = default;
    [SerializeField, Tooltip("ドロップダウン事の壁回転速度")] float[] _rotateSpeeds = new float[5];

    /// <summary>ゲームをプレイ中かどうか </summary>
    bool _isGameing = false;
    /// <summary>マップ生成の関数をセットする </summary>
    event Action _createMap = default;
    /// <summary>ゲームをプレイ中かどうか </summary>
    public bool IsGameing { get => _isGameing; set => _isGameing = value; }
    public GameObject Player { get => _player; }

    /// <summary>マップ生成の関数をセットする </summary>
    public event Action CreateMap
    {
        add { this._createMap += value; }
        remove { this._createMap -= value; }
    }

    public void GameStart()
    {
        _isGameing = true;
    }

    /// <summary>もう一度遊べるようにする </summary>
    public void Restart()
    {
        _createMap();
        _startButton.gameObject.SetActive(true);
    }

    /// <summary>自機をスタート位置にセットする </summary>
    public void SetPlayerStartPoition(Vector2 startPoint)
    {
        _player.transform.position = startPoint;
    }

    /// <summary>壁の回転速度を変える </summary>
    public void ChangeRotateSpeed()
    {
        var value = _speedDropdown.value;

        switch (value)
        {
            case 0:
                _wall.RotateSpeed = _rotateSpeeds[value];
                break;
            case 1:
                _wall.RotateSpeed = _rotateSpeeds[value];
                break;
            case 2:
                _wall.RotateSpeed = _rotateSpeeds[value];
                break;
            case 3:
                _wall.RotateSpeed = _rotateSpeeds[value];
                break;
            case 4:
                _wall.RotateSpeed = _rotateSpeeds[value];
                break;
        }
    }
}
