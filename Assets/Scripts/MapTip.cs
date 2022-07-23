using System.Collections;
using UnityEngine;

/// <summary>�}�b�v�̏�Ԃ��Ǘ�����N���X </summary>
public class MapTip : MonoBehaviour
{
    /// <summary>�`�b�v�̏�� </summary>
    Status _status = Status.Road;
    /// <summary>�S�[���`�b�v���̍� </summary>
    Countries _goalCountry = Countries.None;
    /// <summary>��Ԃɂ���ĐF��ύX����ׂ̃����_���[ </summary>
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    BoxCollider2D _boxCollider => GetComponent<BoxCollider2D>();
    public Status Status 
    {
        get { return _status; }
        set
        {
            _status = value;
            ChangeColor();
        }
    }

    public Countries GoalCountry { get => _goalCountry; set => _goalCountry = value; }

    /// <summary>
    /// ��Ԃɂ���ĐF��ύX
    /// �����蔻���enebled���ύX����
    /// </summary>
    void ChangeColor()
    {
        switch (_status)
        {
            case Status.Road:
                _spriteRenderer.color = Color.black;
                _boxCollider.enabled = false;
                break;
            case Status.Wall:
                _spriteRenderer.color = Color.red;
                _boxCollider.enabled = true;
                break;
            case Status.GoalWall:
                _spriteRenderer.color = Color.yellow;
                _boxCollider.enabled = true;
                break;
            case Status.Goal:
                _spriteRenderer.color = Color.black;
                _boxCollider.enabled = true;
                _boxCollider.isTrigger = true;
                break;
            case Status.Start:
                _spriteRenderer.color = Color.black;
                _boxCollider.enabled = true;
                _boxCollider.isTrigger = true;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().IsGameing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _status == Status.Start)
        {
            _status = Status.Goal;
            Debug.Log("goal�ɕύX����܂���");
        }
        else if (collision.gameObject.CompareTag("Player") && _status == Status.Goal)
        {
            Debug.Log($"Goal {_goalCountry}");
            ResultManager.Instance.GoalCountry = _goalCountry;
            StartCoroutine(WaitTime());
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2f);
        SceneChange.TransitionScene("ResultScene");
    }
}

/// <summary>tip�̏�� </summary>
public enum Status
{
    Wall = 0,
    Road = 1,
    /// <summary>�o�����̖ڈ�ɂȂ�� </summary>
    GoalWall = 2,
    Goal = 3,
    Start = 4,
}
