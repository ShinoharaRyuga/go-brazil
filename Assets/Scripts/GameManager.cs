using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

/// <summary>
/// �Q�[�����Ǘ�����
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _player = default;
    [SerializeField, Tooltip("�X�^�[�g�ʒu")] Transform _startPoint = default;
    [SerializeField, Tooltip("�v���C���[�����삷���")] WallController _wall = default;
    [SerializeField, Tooltip("�Q�[���J�n�{�^��")] Button _startButton = default;
    [SerializeField, Tooltip("�ǂ̉�]���x��ς���ׂ̃h���b�v�_�E��")] TMP_Dropdown _speedDropdown = default;
    [SerializeField, Tooltip("")] TMP_Dropdown _playerFallDropdown = default;
    [SerializeField, Tooltip("�h���b�v�_�E�����̕ǉ�]���x")] float[] _rotateSpeeds = new float[5];
    [SerializeField, Tooltip("�v���C���[�̗����鑬�x")] float[] _playerFallSpeeds = new float[5];
    
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

    /// <summary>������x�V�ׂ�悤�ɂ��� </summary>
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


    /// <summary>���͂��ꂽ�l���v���C���[���x�ɐݒ肷�� </summary>
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

    /// <summary>�ǂ̉�]���x��ς��� </summary>
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
