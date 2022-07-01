using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController = default;
    [SerializeField, Tooltip("スタート位置")] Transform _startPoint = default;
    [SerializeField, Tooltip("プレイヤーが操作する壁")] WallController _wall = default;
    [SerializeField, Tooltip("ゲーム開始ボタン")] Button _startButton = default;
    [SerializeField, Tooltip("壁の回転速度を変える為のドロップダウン")] TMP_Dropdown _speedDropdown = default;
    [SerializeField, Tooltip("ドロップダウン事の壁回転速度")] float[] _rotateSpeeds = new float[5];

    /// <summary>もう一度遊べるようにする </summary>
    public void Restart()
    {
        _playerController.gameObject.SetActive(true);
        _playerController.transform.position = _startPoint.position;
        _wall.transform.rotation = new Quaternion(0, 0, 0, 0);
        _startButton.gameObject.SetActive(true);
        _playerController.Rb2D.velocity = Vector3.zero;
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
