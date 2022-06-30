using UnityEngine;

public class ChangeForceDirection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.SetForceDirection(-transform.parent.transform.up);
        }
    }
}
