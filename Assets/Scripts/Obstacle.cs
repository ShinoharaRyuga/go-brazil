using UnityEngine;

/// <summary>
/// 障害物　
/// プレイヤーが衝突したらプレイヤーを削除する 
/// </summary>
public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().IsGameing = false;
        }
    }
}
