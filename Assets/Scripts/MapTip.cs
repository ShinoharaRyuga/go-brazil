using UnityEngine;

/// <summary>�}�b�v�̏�Ԃ��Ǘ�����N���X </summary>
public class MapTip : MonoBehaviour
{
    /// <summary>�`�b�v�̏�� </summary>
    Status _status = Status.Road;
    /// <summary>��Ԃɂ���ĐF��ύX����ׂ̃����_���[ </summary>
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    public Status Status 
    {
        get { return _status; }
        set
        {
            _status = value;
            ChangeColor();
        }
    }

    /// <summary>��Ԃɂ���ĐF��ύX </summary>
    void ChangeColor()
    {
        switch (_status)
        {
            case Status.Road:
                _spriteRenderer.color = Color.black;
                break;
            case Status.Wall:
                _spriteRenderer.color = Color.red;
                break;
        }
    }
}

/// <summary>tip�̏�� </summary>
public enum Status
{
    Wall,
    Road,
}
