using System.Collections;
using UnityEngine;

/// <summary>マップの状態を管理するクラス </summary>
public class MapTip : MonoBehaviour
{
    /// <summary>チップの状態 </summary>
    Status _status = Status.Road;
    /// <summary>ゴールチップ時の国 </summary>
    Countries _goalCountry = Countries.None;
    BoxCollider2D _boxCollider => GetComponent<BoxCollider2D>();
    /// <summary>状態によって色を変更する為のレンダラー </summary>
    public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();
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

    public bool IsRoad => Status == Status.Road;

    /// <summary>
    /// 状態によって色を変更
    /// 当たり判定のenebledも変更する
    /// </summary>
    void ChangeColor()
    {
        switch (_status)
        {
            case Status.Road:
                SpriteRenderer.color = Color.black;
                _boxCollider.enabled = false;
                break;
            case Status.Wall:
                SpriteRenderer.color = Color.red;
                _boxCollider.enabled = true;
                break;
            case Status.GoalWall:
                SpriteRenderer.color = Color.yellow;
                _boxCollider.enabled = true;
                break;
            case Status.Goal:
                SpriteRenderer.color = Color.black;
                _boxCollider.enabled = true;
                _boxCollider.isTrigger = true;
                break;
            case Status.Start:
                SpriteRenderer.color = Color.black;
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
            Debug.Log("goalに変更されました");
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

/// <summary>tipの状態 </summary>
public enum Status
{
    Wall = 0,
    Road = 1,
    /// <summary>出入口の目印になる壁 </summary>
    GoalWall = 2,
    Goal = 3,
    Start = 4,
}
