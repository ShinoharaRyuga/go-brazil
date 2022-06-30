using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController = default;
    [SerializeField] Transform _startPoint = default;
    [SerializeField] WallController _wall = default;
    [SerializeField] Button _startButton = default;
    [SerializeField] TMP_Dropdown _speedDropdown = default;
    [SerializeField] float[] _rotateSpeeds = new float[5];

    public void Restart()
    {
        _playerController.gameObject.SetActive(true);
        _playerController.transform.position = _startPoint.position;
        _wall.transform.rotation = new Quaternion(0, 0, 0, 0);
        _startButton.gameObject.SetActive(true);
        _playerController.Rb2D.velocity = Vector3.zero;
    }

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
