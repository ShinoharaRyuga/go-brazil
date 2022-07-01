using System.Collections;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField, Tooltip("��]���x")] float _rotateSpeed = 0.1f;
    [SerializeField, Tooltip("��]����")] RotateDirection _rotateDirection = RotateDirection.Normal;
    /// <summary>�O��^�b�`���ꂽX�̈ʒu </summary>
    float _lasttouchpositionX = 0f;

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

        foreach (var touch in Input.touches)
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

    

    enum RotateDirection
    {
        /// <summary>���͂��������ɉ�]����</summary>
        Normal,
        /// <summary>���͂��������Ƃ͋t��]����</summary>
        Reverse
    }
}
