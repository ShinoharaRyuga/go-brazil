using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

/// <summary>
/// ゲームを管理する
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _player = default;
    [SerializeField, Tooltip("スタート位置")] Transform _startPoint = default;
    [SerializeField, Tooltip("プレイヤーが操作する壁")] WallController _wall = default;
    [SerializeField, Tooltip("ゲーム開始ボタン")] Button _startButton = default;
    [SerializeField, Tooltip("壁の回転速度を変える為のドロップダウン")] TMP_Dropdown _speedDropdown = default;
    [SerializeField, Tooltip("")] TMP_Dropdown _playerFallDropdown = default;
    [SerializeField, Tooltip("ドロップダウン事の壁回転速度")] float[] _rotateSpeeds = new float[5];
    [SerializeField, Tooltip("プレイヤーの落ちる速度")] float[] _playerFallSpeeds = new float[5];
    
    event Action _createMap = default;

    public event Action CreateMap
    {
        add { this._createMap += value; }
        remove { this._createMap -= value; }
    }

    public void GameStart()
    {
        _player.AddForce();
    }

    /// <summary>もう一度遊べるようにする </summary>
    public void Restart()
    {
        _createMap();
        _startButton.gameObject.SetActive(true);
        _player.Rb2D.velocity = Vector3.zero;
    }

    public void SetPlayerStartPoition(Vector2 startPoint)
    {
        _player.transform.position = startPoint;
    }


    /// <summary>入力された値をプレイヤー速度に設定する </summary>
    public void ChangePlayerSpeed()
    {
        var value = _playerFallDropdown.value;

        switch (value)
        {
            case 0:
                _player.Speed = _playerFallSpeeds[value];
                break;
            case 1:
                _player.Speed = _playerFallSpeeds[value];
                break;
            case 2:
                _player.Speed = _playerFallSpeeds[value];
                break;
            case 3:
                _player.Speed = _playerFallSpeeds[value];
                break;
            case 4:
                _player.Speed = _playerFallSpeeds[value];
                break;
        }
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
