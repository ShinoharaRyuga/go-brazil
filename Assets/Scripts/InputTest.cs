using UnityEngine;
using TMPro;

/// <summary>�X�}�z�̓��͂������Ă݂� </summary>
public class InputTest : MonoBehaviour
{
    [SerializeField, Tooltip("�^�b�`����Ă���ꏊ�������I�u�W�F�N�g")] GameObject _touchObjectPrefab = default;
    [SerializeField, Tooltip("�^�b�v���ꂽ�񐔂�\���@�f�o�b�O���\������ ")] TextMeshProUGUI _tapCountText = default;
    [SerializeField, Tooltip("�t���b�N���̓e�X�g����׃I�u�W�F�N�g")] GameObject _flickTestObject = default;
    [SerializeField, Tooltip("�f�o�b�O����\��")] TextMeshProUGUI _debugText = default;
    [SerializeField, Tooltip("�{�^���������ꂽ���̃e�L�X�g")] TextMeshProUGUI _buttonDebugText = default;
    [SerializeField, Tooltip("�t���b�N���͂��󂯕t����ŏ��̈ړ�����")] float _minInputFlickDirection = 20f;
    [SerializeField, Tooltip("�t���b�N���͂��󂯕t����ő�̈ړ�����")] float _maxInputFlickDirection = 50f;
    [SerializeField, Tooltip("�t���b�N����������܂ł̎���")] float _activateFlickTime = 1f;

    /// <summary>Instantiate�����N���[��</summary>
    GameObject _touchObject = default;
    /// <summary>�^�b�v�� </summary>
    int _tapCount = 0;
    /// <summary>_flickTestObject��rotate.z�̒l </summary>
    int _flickRotateValue = 0;
    /// <summary>�O��^�b�`���ꂽX�̈ʒu </summary>
    float _lasttouchpositionX = 0f;
    /// <summary>�t���b�N�ł�X�ړ����� </summary>
    float _flickDirectionX = 0f;
    /// <summary>�t���b�N�ł�Y�ړ����� </summary>
    float _flickDirectionY = 0f;
    /// <summary>��ʂɎw���G��Ă���̎��ԁ@�t���b�N���������鎞��</summary>
    float _flickTime = 0f;
    /// <summary>�t���b�N���������鎞�Ԃ̌v�����J�n </summary>
    bool _isflickTimer = false;
    Vector3 _startPoint = Vector3.zero;
    Vector3 _endPoint = Vector3.zero;
    void Start()
    {
        _touchObject = Instantiate(_touchObjectPrefab, Vector2.zero, Quaternion.identity);
    }

    void Update()
    {
        //foreach (var touch in Input.touches)
        //{
        //    var touchposition = new Vector3(touch.position.x, touch.position.y, 10);
        //    touchposition = Camera.main.ScreenToWorldPoint(touchposition);

        //    if (touch.phase == TouchPhase.Moved)
        //    {
        //        _touchObject.transform.position = touchposition;

        //        if (touch.position.x < _lasttouchpositionX) //���Ɉړ�
        //        {
        //            _debugText.text = "���Ɉړ�";
        //        }
        //        else if (touch.position.x > _lasttouchpositionX) //�E�Ɉړ�
        //        {
        //            _debugText.text = "�E�Ɉړ�";
        //        }

        //        _tapCountText.text = $"{touch.position.x} {_lasttouchpositionX}";   //x�ʒu��\��
        //        _lasttouchpositionX = touch.position.x;
        //    }
        //}

        foreach (var touch in Input.touches)    //�t���b�N����
        {
            if (touch.phase == TouchPhase.Began)
            {
                _startPoint = new Vector3(touch.position.x, touch.position.y, 10);
                _isflickTimer = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _endPoint = new Vector3(touch.position.x, touch.position.y, 10);
                GetFlickDistance();
                _buttonDebugText.text = $"�ړ����� X{_flickDirectionX} Y{_flickDirectionY}";
               
                //�t���b�N���͂��o���Ă��邩�ǂ����̔���
                if (_minInputFlickDirection <= _flickDirectionX && _flickDirectionX <= _maxInputFlickDirection ||
                    _minInputFlickDirection <= _flickDirectionY && _flickDirectionY <= _maxInputFlickDirection &&
                    _flickTime <= _activateFlickTime)
                {
                    _flickRotateValue += 90;
                    _flickTestObject.transform.Rotate(0, 0, _flickRotateValue);

                    if (360 <= _flickRotateValue)
                    {
                        _flickRotateValue = 0;
                    }

                    _debugText.text = "�t���b�N����";
                }
                else
                {
                    _debugText.text = "�t���b�N���s";
                }

                _startPoint = Vector3.zero;
                _endPoint = Vector3.zero;
                _flickTime = 0f;
                _isflickTimer = false;
            }
        }

        if (_isflickTimer)
        {
            _flickTime += Time.deltaTime;
            _tapCountText.text = _flickTime.ToString();
        }
    }

    /// <summary>�{�^���̋@�\�������ׂ̊֐� </summary>
    public void DebugButton()
    {
        _buttonDebugText.text = "�{�^���������ꂽ";
    }

    /// <summary>�{�^���̋@�\�������ׂ̊֐� </summary>
    public void ResetButton()
    {
        _buttonDebugText.text = "���Z�b�g";
    }

    /// <summary>�t���b�N�̋��������߂� </summary>
    void GetFlickDistance()
    {
        _flickDirectionX = Mathf.Abs(_endPoint.x - _startPoint.x);
        _flickDirectionY = Mathf.Abs(_endPoint.y - _startPoint.y);
    }
}


///���͂��󂯎��
//foreach (var touch in Input.touches)
//{
//    var touchPhase = touch.phase;

//    switch (touchPhase)
//    {
//        case TouchPhase.Began:
//            _debugText.text = "��ʂɎw���G�ꂽ";
//            break;
//        case TouchPhase.Moved:
//            _debugText.text = "��ʏ�Ŏw��������";
//            break;
//        case TouchPhase.Ended:
//            _debugText.text = "��ʂ���w�����ꂽ";
//            break;
//        case TouchPhase.Stationary:
//            _debugText.text = "�w����ʂɐG��Ă��邪�����Ă͂��Ȃ�";
//            break;
//        case TouchPhase.Canceled:
//            _debugText.text = "�V�X�e�����^�b�`�̒ǐՂ��L�����Z��";
//            break;
//        default:
//            _debugText.text = "�G��Ă��Ȃ�";
//            break;
//    }
//}

//�G��Ă���w�̐�
//if (Input.touchCount == 1)
//{
//    _debugText.text = "������Ă���";
//    _tapCount++;
//    _tapCountText.text = _tapCount.ToString();
//}


