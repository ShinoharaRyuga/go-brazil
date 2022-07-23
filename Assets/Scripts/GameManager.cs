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
    [SerializeField, Tooltip("�}�b�v���㏸������")] MapMove _mapMove = default;
    [SerializeField, Tooltip("�Q�[���J�n�{�^��")] Button _startButton = default;
    [SerializeField, Tooltip("�ǂ̉�]���x�����͂����")] TMP_InputField _rotateInputField = default;
    [SerializeField, Tooltip("�}�b�v�̈ړ����x�����͂����")] TMP_InputField _mapMoveInputField = default;
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

    /// <summary>������x�V�ׂ�悤�ɂ��� </summary>
    public void Restart()
    {
        _createMap();
        _startButton.gameObject.SetActive(true);
        _player.SetActive(true);
        _isGameing = false;
    }

    /// <summary>���@���X�^�[�g�ʒu�ɃZ�b�g���� </summary>
    public void SetPlayerStartPoition(Vector2 startPoint)
    {
        _player.transform.position = startPoint;
    }

    /// <summary>�ǂ̉�]���x��ύX���� </summary>
    public void ChangeRotateSpeed()
    {
        var speed = float.Parse(_rotateInputField.text);
        _wall.RotateSpeed = speed;
    }

    /// <summary>�}�b�v�̏㏸���x��ύX���� </summary>
    public void ChangeMapMoveSpeed()
    {
        var speed = float.Parse(_mapMoveInputField.text);
        _mapMove.MoveValue = speed;
    }
}
