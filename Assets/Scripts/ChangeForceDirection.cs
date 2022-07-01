using UnityEngine;
using Cinemachine;

public class ChangeForceDirection : MonoBehaviour
{
    [SerializeField] CameraController _cameraController = default;
    [SerializeField] WallController _wallController = default;
    [SerializeField] GameObject _green = default;
    [SerializeField] Transform _resetPoint = default;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.SetForceDirection(-transform.parent.transform.up);
            _cameraController.ChangeCameraRotate(_green.transform.localEulerAngles.z);
            Debug.Log(transform.localEulerAngles.z);
            StartCoroutine(_wallController.ResetRotate());
            player.transform.position = _resetPoint.position;

        }
    }
}
