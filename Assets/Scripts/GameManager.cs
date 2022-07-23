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
    [SerializeField, Tooltip("マップを上昇させる")] MapMove _mapMove = default;
    [SerializeField, Tooltip("ゲーム開始ボタン")] Button _startButton = default;
    [SerializeField, Tooltip("壁の回転速度が入力される")] TMP_InputField _rotateInputField = default;
    [SerializeField, Tooltip("マップの移動速度が入力される")] TMP_InputField _mapMoveInputField = default;
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

    private void Start()
    {
        SetInputFieldText();
    }

    private void SetInputFieldText()
    {
        _rotateInputField.text = _wall.RotateSpeed.ToString("F1");
        _mapMoveInputField.text = _mapMove.MoveValue.ToString("F2");
    }

    public void GameStart()
    {
        _isGameing = true;
    }

    public void GameClear()
    {
        _isGameing = false;
    }

    /// <summary>もう一度遊べるようにする </summary>
    public void Restart()
    {
        _createMap();
        _startButton.gameObject.SetActive(true);
        _player.SetActive(true);
        _isGameing = false;
    }

    /// <summary>自機をスタート位置にセットする </summary>
    public void SetPlayerStartPoition(Vector2 startPoint)
    {
        _player.transform.position = startPoint;
    }

    /// <summary>壁の回転速度を変更する </summary>
    public void ChangeRotateSpeed()
    {
        var speed = float.Parse(_rotateInputField.text);
        _wall.RotateSpeed = speed;
    }

    /// <summary>マップの上昇速度を変更する </summary>
    public void ChangeMapMoveSpeed()
    {
        var speed = float.Parse(_mapMoveInputField.text);
        _mapMove.MoveValue = speed;
    }
}
