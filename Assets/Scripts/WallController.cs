using System.Collections;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField, Tooltip("��]���x")] float _rotateSpeed = 0.1f;
    [SerializeField, Tooltip("��]����")] RotateDirection _rotateDirection = RotateDirection.Normal;
    [SerializeField, Tooltip("�t���b�N���͂��󂯕t����ŏ��̈ړ�����")] float _minInputFlickDirection = 20f;
    [SerializeField, Tooltip("�t���b�N���͂��󂯕t����ő�̈ړ�����")] float _maxInputFlickDirection = 50f;
    [SerializeField, Tooltip("�t���b�N����������܂ł̎���")] float _activateFlickTime = 1f;
    /// <summary>��ʂɎw���G�ꂽ���̏ꏊ </summary>
    Vector3 _startPoint = Vector3.zero;
    /// <summary>��ʂɎw�����ꂽ���̏ꏊ </summary>
    Vector3 _endPoint = Vector3.zero;
    /// <summary>_flickTestObject��rotate.z�̒l </summary>
    int _flickRotateValue = 0;
    /// <summary>�t���b�N�ł�X�ړ����� </summary>
    float _flickDirectionX = 0f;
    /// <summary>�t���b�N�ł�Y�ړ����� </summary>
    float _flickDirectionY = 0f;
    /// <summary>�O��^�b�`���ꂽX�̈ʒu </summary>
    float _lasttouchpositionX = 0f;
    /// <summary>��ʂɎw���G��Ă���̎��ԁ@�t���b�N���������鎞��</summary>
    float _flickTime = 0f;
    /// <summary>�t���b�N���������鎞�Ԃ̌v�����J�n </summary>
    bool _isflickTimer= false;

    public float RotateSpeed
    {
        get { return _rotateSpeed; }
        set
        {
            if (0 < value)
            {
                _rotateSpeed = value;
            }
        }
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");

        if (h == 1)
        {
            ChangeMoveDirection(true);
        }
        else if (h == -1)
        {
            ChangeMoveDirection(false);
        }

        foreach (var touch in Input.touches)    //�X���C�v����
        {
            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < _lasttouchpositionX) //���Ɉړ�
                {
                    ChangeMoveDirection(false);
                }
                else if (touch.position.x > _lasttouchpositionX) //�E�Ɉړ�
                {
                    ChangeMoveDirection(true);
                }

                _lasttouchpositionX = touch.position.x;
            }
        }

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
            
                //�t���b�N���͂��o���Ă��邩�ǂ����̔���
                if (_minInputFlickDirection <= _flickDirectionX && _flickDirectionX <= _maxInputFlickDirection      //X���̃t���b�N
                    || _minInputFlickDirection <= _flickDirectionY && _flickDirectionY <= _maxInputFlickDirection   //Y���̃t���b�N
                    && _flickTime <= _activateFlickTime)   //�t���b�N���������邩�ǂ���
                {
                    _flickRotateValue += 90;
                     transform.Rotate(0, 0, _flickRotateValue);

                    if (360 <= _flickRotateValue)
                    {
                        _flickRotateValue = 0;
                    }

                    _flickTime = 0f;
                    _isflickTimer = false;
                }
               
                _startPoint = Vector3.zero;
                _endPoint = Vector3.zero;
            }
        }

        if (_isflickTimer)
        {
            _flickTime += Time.deltaTime;
        }
    }

    /// <summary>_rotateDirection�ɂ���ĉ�]������ς���</summary>
    /// <param name="InputDirection">true=�E���͂���Ă���@false=�����͂���Ă���</param>
    public void ChangeMoveDirection(bool InputDirection)
    {
        if (_rotateDirection == RotateDirection.Normal)
        {
            if (InputDirection)
            {
                transform.Rotate(0, 0, _rotateSpeed);
            }
            else
            {
                transform.Rotate(0, 0, -_rotateSpeed);
            }
        }
        else if (_rotateDirection == RotateDirection.Reverse)
        {
            if (InputDirection)
            {
                transform.Rotate(0, 0, -_rotateSpeed);
            }
            else
            {
                transform.Rotate(0, 0, _rotateSpeed);
            }
        }
    } 

    public IEnumerator ResetRotate()
    {
        yield return new WaitForSeconds(2f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("���Z�b�g");
    }

    /// <summary>�t���b�N�̋��������߂� </summary>
    void GetFlickDistance()
    {
        _flickDirectionX = Mathf.Abs(_endPoint.x - _startPoint.x);
        _flickDirectionY = Mathf.Abs(_endPoint.y - _startPoint.y);
    }


    enum RotateDirection
    {
        /// <summary>���͂��������ɉ�]����</summary>
        Normal,
        /// <summary>���͂��������Ƃ͋t��]����</summary>
        Reverse
    }
}
