using UnityEngine;

/// <summary>
/// ��Q���@
/// �v���C���[���Փ˂�����v���C���[���폜���� 
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
