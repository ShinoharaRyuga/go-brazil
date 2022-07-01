using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController = default;
    [SerializeField, Tooltip("�X�^�[�g�ʒu")] Transform _startPoint = default;
    [SerializeField, Tooltip("�v���C���[�����삷���")] WallController _wall = default;
    [SerializeField, Tooltip("�Q�[���J�n�{�^��")] Button _startButton = default;
    [SerializeField, Tooltip("�ǂ̉�]���x��ς���ׂ̃h���b�v�_�E��")] TMP_Dropdown _speedDropdown = default;
    [SerializeField, Tooltip("�h���b�v�_�E�����̕ǉ�]���x")] float[] _rotateSpeeds = new float[5];

    /// <summary>������x�V�ׂ�悤�ɂ��� </summary>
    public void Restart()
    {
        _playerController.gameObject.SetActive(true);
        _playerController.transform.position = _startPoint.position;
        _wall.transform.rotation = new Quaternion(0, 0, 0, 0);
        _startButton.gameObject.SetActive(true);
        _playerController.Rb2D.velocity = Vector3.zero;
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
