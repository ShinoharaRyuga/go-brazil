using UnityEngine;

/// <summary>�X�R�A�A�b�v�A�C�e�� </summary>
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
