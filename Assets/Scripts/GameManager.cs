using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Q�[�����Ǘ�����
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _player = default;
    [SerializeField, Tooltip("�v���C���[�����삷���")] WallController _wall = default;
    [SerializeField, Tooltip("�Q�[���J�n�{�^��")] Button _startButton = default;
    [SerializeField, Tooltip("�ǂ̉�]���x��ς���ׂ̃h���b�v�_�E��")] TMP_Dropdown _speedDropdown = default;
    [SerializeField, Tooltip("")] TMP_Dropdown _playerFallDropdown = default;
    [SerializeField, Tooltip("�h���b�v�_�E�����̕ǉ�]���x")] float[] _rotateSpeeds = new float[5];

    /// <summary>�Q�[�����v���C�����ǂ��� </summary>
    bool _isGameing = false;
    /// <summary>�}�b�v�����̊֐����Z�b�g���� </summary>
    event Action _createMap = default;
    /// <summary>�Q�[�����v���C�����ǂ��� </summary>
    public bool IsGameing { get => _isGameing; set => _isGameing = value; }
    public GameObject Player { get => _player; }

    /// <summary>�}�b�v�����̊֐����Z�b�g���� </summary>
    public event Action CreateMap
    {
        add { this._createMap += value; }
        remove { this._createMap -= value; }
    }

    public void GameStart()
    {
        _isGameing = true;
    }

    /// <summary>������x�V�ׂ�悤�ɂ��� </summary>
    public void Restart()
    {
        _createMap();
        _startButton.gameObject.SetActive(true);
    }

    /// <summary>���@���X�^�[�g�ʒu�ɃZ�b�g���� </summary>
    public void SetPlayerStartPoition(Vector2 startPoint)
    {
        _player.transform.position = startPoint;
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
